using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CustomPropertyDrawer(typeof(MainToolbarElementTypeDropdownAttribute))]
    internal class MainToolbarElementTypeDropdownDrawer : PropertyDrawer
    {
        private struct TypeWithShortName
        {
            public Type Type;
            public string ShortName;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var types = ServicesAndRepositories.MainToolbarElementRepository
                .GetAll()
                .Select(mainToolbarElement => mainToolbarElement.VisualElement.GetType());

            var shortenedTypes = GetShortsForTypes(types);

            var typesByShortName = shortenedTypes.ToDictionary(shortenedType => shortenedType.ShortName, shortenedType => shortenedType.Type);

            var typeNamesList = shortenedTypes.Select(typeWithShortName => typeWithShortName.ShortName).ToList();

            var popupField = new PopupField<string>(
                choices: typeNamesList,
                0);

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = typesByShortName[popupField.value].FullName;
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                var shortenedTypeOfCurrentValue = shortenedTypes
                    .FirstOrDefault(shortenedType => shortenedType.Type.FullName == property.stringValue);

                if (typeNamesList.Contains(shortenedTypeOfCurrentValue.ShortName))
                    popupField.SetValueWithoutNotify(shortenedTypeOfCurrentValue.ShortName);
                else
                    popupField.SetValueWithoutNotify("");
            }

            popupField.RegisterCallback<ChangeEvent<string>>(ev =>
            {
                property.stringValue = typesByShortName[ev.newValue].FullName;
                property.serializedObject.ApplyModifiedProperties();
            });

            return popupField;
        }

        private IEnumerable<TypeWithShortName> GetShortsForTypes(IEnumerable<Type> types)
        {
            return GetShortsForTypesRecursively(types.Select(type => new TypeWithShortName()
            {
                Type = type,
                ShortName = type.Name
            }));
        }

        private IEnumerable<TypeWithShortName> GetShortsForTypesRecursively(IEnumerable<TypeWithShortName> shortenedTypes)
        {
            var groupsByShortName = shortenedTypes.GroupBy(shortenedType => shortenedType.ShortName);

            if (!HasRepeatedNames(groupsByShortName) || !CanBeShortenedFurther(groupsByShortName))
                return shortenedTypes;

            var notRepeated = groupsByShortName.Where(group => group.Count() == 1)
                .SelectMany(group => group);

            var repeated = groupsByShortName.Where(group => group.Count() > 1)
                .SelectMany(group => group);

            var newShortened = repeated.Select(repeatedElement => DoDeambiguationStep(repeatedElement));

            return GetShortsForTypesRecursively(notRepeated.Concat(newShortened));
        }

        private bool HasRepeatedNames(IEnumerable<IGrouping<string, TypeWithShortName>> groups)
        {
            return !groups.All(group => group.Count() == 1);
        }

        private bool CanBeShortenedFurther(IEnumerable<IGrouping<string, TypeWithShortName>> groups)
        {
            return groups.Where(group => group.Count() > 1)
                .Any(group => CanBeShortened(group.First()));
        }

        private bool CanBeShortened(TypeWithShortName shortenedType)
        {
            return shortenedType.Type.FullName != shortenedType.ShortName;
        }

        private TypeWithShortName DoDeambiguationStep(TypeWithShortName shortenedType)
        {
            var typeParts = shortenedType.Type.Namespace.Split('.');

            for(int i = typeParts.Length - 1; i >= 0; i--)
            {
                var typePart = typeParts[i];
                if (!shortenedType.ShortName.Contains(typePart))
                {
                    var newShortName = typePart + '.' + shortenedType.ShortName;
                    return new TypeWithShortName()
                    {
                        Type = shortenedType.Type,
                        ShortName = newShortName
                    };
                }
            }

            return shortenedType;
        }
    }
}
