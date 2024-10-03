using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class SerializableValue<T>
    {
        private const string JSON_UTILITY_DEFAULT_STRING = "{}";

        private string _saveKey;
        public T DefaultValue { get; set; }
        public T Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        public SerializableValue(string saveKey) : this(saveKey, default)
        {
            
        }

        public SerializableValue(string saveKey, T defaultValue) : this(saveKey, defaultValue, defaultValue)
        {

        }

        public SerializableValue(string saveKey, T defaultValue, T initialValue)
        {
            _saveKey = saveKey;
            DefaultValue = defaultValue;
            SetValue(initialValue);
        }

        private T GetValue()
        {
            var serializedValue = EditorPrefs.GetString(_saveKey);

            try
            {
                var value = JsonUtility.FromJson<T>(serializedValue);

                if (JsonUtility.ToJson(value) == JSON_UTILITY_DEFAULT_STRING)
                    return JsonConvert.DeserializeObject<T>(serializedValue);

                return value;
            }
            catch
            {
                var value = JsonConvert.DeserializeObject<T>(serializedValue);

                return value;
            }
        }

        private void SetValue(T value)
        {
            var serializedValue = JsonUtility.ToJson(value);

            if (serializedValue == JSON_UTILITY_DEFAULT_STRING)
                serializedValue = JsonConvert.SerializeObject(value);

            EditorPrefs.SetString(_saveKey, serializedValue);
        }

        public override string ToString()
        {
            var serializedType = typeof(T);
            var toStringMethod = serializedType.GetMethod(nameof(ToString), Type.EmptyTypes);

            if(toStringMethod.DeclaringType != serializedType)
                return EditorPrefs.GetString(_saveKey);

            return Value.ToString();
        }
    }
}
