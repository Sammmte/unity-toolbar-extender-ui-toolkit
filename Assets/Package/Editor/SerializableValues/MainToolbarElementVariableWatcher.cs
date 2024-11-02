using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementVariableWatcher
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private const double SERIALIZE_EVERY_SECONDS = 0.5f;
        private static double _nextSerializeTime = SERIALIZE_EVERY_SECONDS;

        private MainToolbarElementWithSerializableVariables[] _elementsWithVariables;
        private readonly IMainToolbarElementVariableRepository _repository;

        public MainToolbarElementVariableWatcher(IMainToolbarElementVariableRepository repository)
        {
            _repository = repository;
        }

        public void RestoreValues(MainToolbarElement[] elements)
        {
            _elementsWithVariables = GetElementsWithVariables(elements);

            Debug.Log(_elementsWithVariables.Length);
        }

        public void Update()
        {
            if (EditorApplication.timeSinceStartup > _nextSerializeTime)
            {
                _nextSerializeTime = EditorApplication.timeSinceStartup + SERIALIZE_EVERY_SECONDS;
                WatchChanges();
            }
        }

        private void WatchChanges()
        {
            var anyChange = false;

            foreach(var element in _elementsWithVariables)
            {
                if (element.DidChange())
                {
                    anyChange = true;
                    _repository.Set(ToSerializable(element));
                }
            }

            if(anyChange)
                _repository.Save();
        }

        private MainToolbarElementWithSerializableVariables[] GetElementsWithVariables(MainToolbarElement[] elements)
        {
            return elements.Select(element => new MainToolbarElementWithSerializableVariables
            {
                MainToolbarElement = element,
                Fields = GetSerializableFields(element),
                Properties = GetSerializableProperties(element)
            })
                .Where(serializableElement => serializableElement.Fields.Any() || serializableElement.Properties.Any())
                .ToArray();
        }

        private FieldSerializableVariable[] GetSerializableFields(MainToolbarElement element)
        {
            return element.VisualElement.GetType().GetFields(BINDING_FLAGS)
                .Where(field => field.GetCustomAttribute<SerializeAttribute>() != null)
                .Select(field => new FieldSerializableVariable(element, field))
                .ToArray();
        }

        private PropertySerializableVariable[] GetSerializableProperties(MainToolbarElement element)
        {
            return element.VisualElement.GetType().GetProperties(BINDING_FLAGS)
                .Where(property => property.GetCustomAttribute<SerializeAttribute>() != null)
                .Select(property => new PropertySerializableVariable(element, property))
                .ToArray();
        }

        private SerializableElement ToSerializable(MainToolbarElementWithSerializableVariables elementWithVariables)
        {
            return new SerializableElement()
            {
                ElementFullTypeName = elementWithVariables.MainToolbarElement.VisualElement.GetType().FullName,
                Variables = elementWithVariables.Fields.Select(f => new SerializableVariable()
                {
                    Type = ValueHolderType.Field,
                    Key = f.Field.Name,
                    Value = f.Get()
                }).Concat(elementWithVariables.Properties.Select(p => new SerializableVariable()
                {
                    Type = ValueHolderType.Property,
                    Key = p.Property.Name,
                    Value = p.Get()
                })).ToArray()
            };
        }
    }
}
