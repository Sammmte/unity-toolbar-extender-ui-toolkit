using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class EditorPrefsSerializableValuesRepository : ISerializableValuesRepository
    {
        private const string NO_VALUE_FOUND_STRING = "NO_VALUE_FOUND";

        private readonly ISerializableValuesSerializer _serializer;

        public EditorPrefsSerializableValuesRepository(ISerializableValuesSerializer serializer)
        {
            _serializer = serializer;
        }

        public Maybe<T> Get<T>(string saveKey)
        {
            var serializedValue = EditorPrefs.GetString(saveKey, NO_VALUE_FOUND_STRING);

            if (!IsValidString(serializedValue))
                return Maybe<T>.None();

            return _serializer.Deserialize<T>(serializedValue);
        }

        private bool IsValidString(string serializedValue)
        {
            return !string.IsNullOrEmpty(serializedValue) && serializedValue != NO_VALUE_FOUND_STRING;
        }

        public void Save<T>(string saveKey, T value)
        {
            var maybeSerializedValue = _serializer.Serialize<T>(value);

            if(!maybeSerializedValue.HasValue)
            {
                Debug.LogWarning($"Value with savekey {saveKey} of type {typeof(T).Name} could not be serialized");
                return;
            }

            EditorPrefs.SetString(saveKey, maybeSerializedValue.Value);
        }
    }
}
