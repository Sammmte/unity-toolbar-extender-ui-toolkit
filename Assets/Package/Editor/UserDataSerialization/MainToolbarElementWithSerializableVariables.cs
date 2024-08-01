using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementWithSerializableVariables
    {
        public MainToolbarElement MainToolbarElement { get; }
        public int VariableCount => _serializableFields.Length;

        private FieldInfo[] _serializableFields = new FieldInfo[0];
        private Dictionary<string, object> _valuesSnapshot = new Dictionary<string, object>();

        public MainToolbarElementWithSerializableVariables(MainToolbarElement mainToolbarElement)
        {
            MainToolbarElement = mainToolbarElement;

            LoadSerializableVariables();
        }

        private void LoadSerializableVariables()
        {
            var mainToolbarElementInnerType = MainToolbarElement.VisualElement.GetType();

            _serializableFields = mainToolbarElementInnerType.GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => field.GetCustomAttribute<SerializeAttribute>(false) != null)
                .ToArray();

            SaveValuesSnapshot();
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

                field.SetValue(MainToolbarElement.VisualElement, keyValue.Value);
            }
        }

        public MainToolbarElementSerializedData GetSerializedData()
        {
            return new MainToolbarElementSerializedData(MainToolbarElement.Id,
                _valuesSnapshot.Select(keyValue => new MainToolbarElementSerializedKeyValue(keyValue.Key, keyValue.Value)
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
                    if (!currentSnapshot.Equals(currentValue))
                        SetAsChangedAndUpdate(field);
                }
            }

            void SetAsChangedAndUpdate(FieldInfo field)
            {
                anyChange = true;
                _valuesSnapshot[field.Name] = field.GetValue(MainToolbarElement.VisualElement);
            }

            return anyChange;
        }
    }
}
