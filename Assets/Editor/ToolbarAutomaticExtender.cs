using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class ToolbarAutomaticExtender
    {
        private const int TOOLBAR_LEFT_CONTAINER_MAX_WIDTH_PERCENTAGE = 10;
        private const int TOOLBAR_RIGHT_CONTAINER_MAX_WIDTH_PERCENTAGE = 17;

        public static VisualElement LeftCustomContainer { get; private set; } = CreateContainer("ToolbarAutomaticExtenderLeftContainer", FlexDirection.RowReverse);
        public static VisualElement RightCustomContainer { get; private set; } = CreateContainer("ToolbarAutomaticExtenderRightContainer", FlexDirection.Row);

        static ToolbarAutomaticExtender()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());

            var elementsWithAttributes = GetMainToolbarElements(allTypes);

            if (elementsWithAttributes.Count() == 0)
                return;

            var leftElements = elementsWithAttributes.Where(tuple => tuple.Attribute.Align == ToolbarAlign.Left);
            var rightElements = elementsWithAttributes.Where(tuple => tuple.Attribute.Align == ToolbarAlign.Right);

            var leftGroups = leftElements.GroupBy(tuple => tuple.Attribute.Group);
            var rightGroups = rightElements.GroupBy(tuple => tuple.Attribute.Group);

            AddSingleElementOrGroupElement(leftGroups, LeftCustomContainer);
            AddSingleElementOrGroupElement(rightGroups, RightCustomContainer);

            ToolbarWrapper.OnNativeToolbarWrapped += () =>
            {
                ToolbarWrapper.LeftContainer.style.maxWidth = Length.Percent(TOOLBAR_LEFT_CONTAINER_MAX_WIDTH_PERCENTAGE);
                ToolbarWrapper.LeftContainer.style.justifyContent = Justify.FlexStart;

                ToolbarWrapper.RightContainer.style.maxWidth = Length.Percent(TOOLBAR_RIGHT_CONTAINER_MAX_WIDTH_PERCENTAGE);
                ToolbarWrapper.RightContainer.style.justifyContent = Justify.FlexStart;

                ToolbarWrapper.CenterContainer.style.flexGrow = 1;

                ToolbarWrapper.CenterContainer.Insert(0, LeftCustomContainer);
                ToolbarWrapper.CenterContainer.Add(RightCustomContainer);
            };
        }

        private static void AddSingleElementOrGroupElement(IEnumerable<IGrouping<string, (VisualElement Element, MainToolbarElementAttribute Attribute)>> groups, VisualElement container)
        {
            foreach (var group in groups)
            {
                if (string.IsNullOrEmpty(group.Key))
                {
                    foreach (var elementWithAttribute in group)
                    {
                        container.Add(elementWithAttribute.Element);
                    }
                }
                else
                {
                    var groupElement = new GroupElement(group.Key,
                        group.ToArray().Select(tuple => tuple.Element).ToArray());

                    container.Add(groupElement);
                }
            }
        }

        private static (VisualElement Element, MainToolbarElementAttribute Attribute)[] GetMainToolbarElements(IEnumerable<Type> types)
        {
            return FilterRawElements(types)
                .Concat(GetElementsFromProviders(types))
                .ToArray();
        }

        private static IEnumerable<(VisualElement Element, MainToolbarElementAttribute Attribute)> FilterRawElements(IEnumerable<Type> allTypes)
        {
            return allTypes
                .Where(type => IsValidVisualElementType(type))
                .Select(type =>
                {
                    var elementInstance = (VisualElement)Activator.CreateInstance(type);
                    var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();

                    return (elementInstance, attribute);
                });
        }

        private static IEnumerable<(VisualElement Element, MainToolbarElementAttribute Attribute)> GetElementsFromProviders(IEnumerable<Type> allTypes)
        {
            return allTypes
                .Where(type => IsValidElementProviderType(type))
                    .Select(type =>
                    {
                        var providerInstance = (IMainToolbarElementProvider)Activator.CreateInstance(type);
                        var attribute = type.GetCustomAttribute<MainToolbarElementAttribute>();
                        var element = providerInstance.GetElement(!string.IsNullOrEmpty(attribute.Group));

                        return (element, attribute);
                    }
                );
        }

        private static bool IsValidVisualElementType(Type type)
        {
            var visualElementType = typeof(VisualElement);

            return visualElementType != type &&
                visualElementType.IsAssignableFrom(type) &&
                !type.IsAbstract &&
                type.GetCustomAttribute<MainToolbarElementAttribute>() != null;
        }

        private static bool IsValidElementProviderType(Type type)
        {
            var elementProviderType = typeof(IMainToolbarElementProvider);

            return elementProviderType != type &&
                elementProviderType.IsAssignableFrom(type) &&
                !type.IsAbstract &&
                type.GetCustomAttribute<MainToolbarElementAttribute>() != null;
        }

        private static VisualElement CreateContainer(string name, FlexDirection flexDirection)
        {
            var container = new VisualElement()
            {
                name = name,
                style = {
                    flexGrow = 1,
                    flexDirection = flexDirection,
                    alignItems = Align.Center,
                    alignContent = Align.Center,
                    maxWidth = Length.Percent(50)
                }
            };

            return container;
        }
    }
}