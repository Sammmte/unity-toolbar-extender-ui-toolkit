using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class JsonEditorPrefsMainToolbarElementSerializedDataRepository : IMainToolbarElementSerializedDataRepository
    {
        private struct SerializableMainToolbarElementSerializedData
        {
            public string Id;
            public SerializableMainToolbarElementSerializedKeyValue[] KeyValues;
        }

        private struct SerializableMainToolbarElementSerializedKeyValue
        {
            public string Key;
            public string SerializedValue;
        }

        private const string SAVE_KEY = "main-toolbar-element-user-serialized-data";

        private Dictionary<string, MainToolbarElementSerializedData> _serializedData;

        public JsonEditorPrefsMainToolbarElementSerializedDataRepository()
        {
            _serializedData = LoadSerializedData();
        }

        public void Clear()
        {
            _serializedData.Clear();
            DeleteSave();
        }

        public MainToolbarElementSerializedData[] GetAll()
        {
            return _serializedData.Values.ToArray();
        }

        public void Set(MainToolbarElementSerializedData[] serializedData)
        {
            foreach (var elementData in serializedData)
            {
                if (_serializedData.ContainsKey(elementData.Id))
                    _serializedData[elementData.Id] = elementData;
                else
                    _serializedData.Add(elementData.Id, elementData);
            }

            SaveSerializedData();
        }

        private Dictionary<string, MainToolbarElementSerializedData> LoadSerializedData()
        {
            var json = JsonEditorPrefs.UserDataRepository.GetString(SAVE_KEY, "{}");

            var serializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, SerializableMainToolbarElementSerializedData>>(json);

            return serializedDictionary.Values
                .ToDictionary(serialized => serialized.Id,
                serialized => FromSerialized(serialized));
        }

        private void SaveSerializedData()
        {
            var serializableDictionary = _serializedData.Values.ToDictionary(
                mainToolbarElementSerializedData => mainToolbarElementSerializedData.Id,
                mainToolbarElementSerializedData => ToSerialized(mainToolbarElementSerializedData)
                );

            var json = JsonConvert.SerializeObject(serializableDictionary);

            JsonEditorPrefs.UserDataRepository.SetString(SAVE_KEY, json);
        }

        private MainToolbarElementSerializedData FromSerialized(SerializableMainToolbarElementSerializedData serializable)
        {
            return new MainToolbarElementSerializedData(serializable.Id, FromSerialized(serializable.KeyValues));
        }

        private SerializableMainToolbarElementSerializedData ToSerialized(MainToolbarElementSerializedData serializedData)
        {
            return new SerializableMainToolbarElementSerializedData()
            {
                Id = serializedData.Id,
                KeyValues = ToSerialized(serializedData.KeyValues)
            };
        }

        private MainToolbarElementSerializedKeyValue[] FromSerialized(SerializableMainToolbarElementSerializedKeyValue[] serializedKeyValues)
        {
            return serializedKeyValues
                .Select(keyValue => new MainToolbarElementSerializedKeyValue(
                    keyValue.Key, keyValue.SerializedValue))
                .ToArray();
        }

        private SerializableMainToolbarElementSerializedKeyValue[] ToSerialized(MainToolbarElementSerializedKeyValue[] keyValues)
        {
            return keyValues
                .Select(keyValue => new SerializableMainToolbarElementSerializedKeyValue()
                {
                    Key = keyValue.Key,
                    SerializedValue = keyValue.SerializedValue
                })
                .ToArray();
        }

        private void DeleteSave()
        {
            JsonEditorPrefs.UserDataRepository.DeleteKey(SAVE_KEY);
        }
    }
}
