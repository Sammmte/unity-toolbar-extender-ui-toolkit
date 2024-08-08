using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementWithSerializableVariables
    {
        public MainToolbarElement MainToolbarElement { get; }
        public int VariableCount => _serializableFields.Length;

        private FieldInfo[] _serializableFields = new FieldInfo[0];
        private Dictionary<string, object> _valuesSnapshot = new Dictionary<string, object>();
        private readonly IMainToolbarElementDataSerializer _serializer;
        private readonly IClonator _clonator;

        public MainToolbarElementWithSerializableVariables(MainToolbarElement mainToolbarElement,
            IMainToolbarElementDataSerializer serializer, IClonator clonator)
        {
            MainToolbarElement = mainToolbarElement;
            _serializer = serializer;
            _clonator = clonator;
            LoadSerializableVariables();
            SaveValuesSnapshot();
        }

        private void LoadSerializableVariables()
        {
            var mainToolbarElementInnerType = MainToolbarElement.VisualElement.GetType();

            _serializableFields = mainToolbarElementInnerType.GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => field.GetCustomAttribute<SerializeAttribute>(false) != null)
                .Where(field => SerializableTypesHelper.IsValidType(field.FieldType))
                .ToArray();
        }

        private void SaveValuesSnapshot()
        {
            foreach(var field in _serializableFields)
            {
                _valuesSnapshot[field.Name] = field.GetValue(MainToolbarElement.VisualElement);
            }
        }

        public void LoadFromSerializedData(MainToolbarElementSerializedData serializedData)
        {
            foreach(var field in _serializableFields)
            {
                var keyValue = serializedData.KeyValues.FirstOrDefault(keyValue => keyValue.Key == field.Name);

                if (string.IsNullOrEmpty(keyValue.Key))
                    continue;

                var retrievedValue = _serializer.Deserialize(keyValue.SerializedValue, field.FieldType);

                field.SetValue(MainToolbarElement.VisualElement, retrievedValue);   
            }
        }

        public MainToolbarElementSerializedData GetSerializedData()
        {
            return new MainToolbarElementSerializedData(MainToolbarElement.Id,
                _valuesSnapshot.Select(keyValue => new MainToolbarElementSerializedKeyValue(keyValue.Key, _serializer.Serialize(keyValue.Value))
                ).ToArray());
        }

        public bool CheckAndUpdateModifiedValues()
        {
            var anyChange = false;

            foreach(var field in _serializableFields)
            {
                var currentSnapshot = _valuesSnapshot[field.Name];
                var currentValue = field.GetValue(MainToolbarElement.VisualElement);

                if (currentSnapshot == null)
                {
                    if (currentValue != null)
                        SetAsChangedAndUpdate(field);
                }
                else
                {
                    if (!SerializableTypesHelper.AreEqual(currentSnapshot, currentValue))
                        SetAsChangedAndUpdate(field);
                }
            }

            void SetAsChangedAndUpdate(FieldInfo field)
            {
                anyChange = true;
                _valuesSnapshot[field.Name] = _clonator.Clone(field.GetValue(MainToolbarElement.VisualElement));
            }

            return anyChange;
        }
    }
}
