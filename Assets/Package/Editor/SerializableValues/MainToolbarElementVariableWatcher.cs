using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementVariableWatcher
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private const double SERIALIZE_EVERY_SECONDS = 0.5f;
        private static double _nextSerializeTime = SERIALIZE_EVERY_SECONDS;

        private ElementVariables[] _elementsWithVariables;
        private readonly IMainToolbarElementVariableRepository _repository;
        private readonly IValueSerializer _valueSerializer;

        public MainToolbarElementVariableWatcher(IMainToolbarElementVariableRepository repository, IValueSerializer valueSerializer)
        {
            _repository = repository;
            _valueSerializer = valueSerializer;
        }

        public void RestoreValues(MainToolbarElement[] elements)
        {
            _elementsWithVariables = GetElementsWithVariables(elements);

            var serializedElements = _repository.GetAll();

            foreach(var element in _elementsWithVariables)
            {
                var matchingSerialized = serializedElements.FirstOrDefault(serialized => serialized.ElementFullTypeName == 
                    element.MainToolbarElement.VisualElement.GetType().FullName);

                if(matchingSerialized != null)
                {
                    RestoreFields(element, matchingSerialized);
                    RestoreProperties(element, matchingSerialized);
                }
            }

            SaveCurrentState();
        }

        private void RestoreFields(ElementVariables element, SerializableElement matchingSerialized)
        {
            foreach (var field in element.Fields)
            {
                var matchingField = matchingSerialized.Variables.FirstOrDefault(v =>
                    v.Key == field.Field.Name &&
                    v.Type == ValueHolderType.Field &&
                    v.ValueType == field.Field.FieldType);

                if (matchingField.Key != null)
                {
                    field.Set(matchingField.Value);
                }
            }
        }

        private void RestoreProperties(ElementVariables element, SerializableElement matchingSerialized)
        {
            foreach (var property in element.Properties)
            {
                var matchingField = matchingSerialized.Variables.FirstOrDefault(v =>
                    v.Key == property.Property.Name &&
                    v.Type == ValueHolderType.Property &&
                    v.ValueType == property.Property.PropertyType);

                if (matchingField.Key != null)
                {
                    property.Set(matchingField.Value);
                }
            }
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
                    element.UpdateValues();
                    _repository.Set(ToSerializable(element));
                }
            }

            if(anyChange)
                _repository.Save();
        }

        private void SaveCurrentState()
        {
            _repository.SetAll(_elementsWithVariables.Select(e => ToSerializable(e)).ToArray());

            _repository.Save();
        }

        private ElementVariables[] GetElementsWithVariables(MainToolbarElement[] elements)
        {
            return elements.Select(element => new ElementVariables
            {
                MainToolbarElement = element,
                Fields = GetSerializableFields(element),
                Properties = GetSerializableProperties(element)
            })
                .Where(serializableElement => serializableElement.Fields.Any() || serializableElement.Properties.Any())
                .ToArray();
        }

        private FieldVariable[] GetSerializableFields(MainToolbarElement element)
        {
            return element.VisualElement.GetType().GetFields(BINDING_FLAGS)
                .Where(field => field.GetCustomAttribute<SerializeAttribute>() != null)
                .Select(field => new FieldVariable(element, field, _valueSerializer))
                .ToArray();
        }

        private PropertyVariable[] GetSerializableProperties(MainToolbarElement element)
        {
            return element.VisualElement.GetType().GetProperties(BINDING_FLAGS)
                .Where(property => property.GetCustomAttribute<SerializeAttribute>() != null)
                .Select(property => new PropertyVariable(element, property, _valueSerializer))
                .ToArray();
        }

        private SerializableElement ToSerializable(ElementVariables elementWithVariables)
        {
            return new SerializableElement()
            {
                ElementFullTypeName = elementWithVariables.MainToolbarElement.VisualElement.GetType().FullName,
                Variables = elementWithVariables.Fields.Select(f => new SerializableVariable()
                {
                    Type = ValueHolderType.Field,
                    Key = f.Field.Name,
                    ValueType = f.Field.FieldType,
                    Value = f.Get()
                }).Concat(elementWithVariables.Properties.Select(p => new SerializableVariable()
                {
                    Type = ValueHolderType.Property,
                    Key = p.Property.Name,
                    ValueType = p.Property.PropertyType,
                    Value = p.Get()
                })).ToArray()
            };
        }
    }
}
