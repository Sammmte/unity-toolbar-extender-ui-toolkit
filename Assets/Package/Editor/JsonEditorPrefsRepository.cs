using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class JsonEditorPrefsRepository
    {
        private string _editorPrefsKey;
        private Dictionary<string, object> _savedValues;

        public JsonEditorPrefsRepository(string editorPrefsKey)
        {
            _editorPrefsKey = editorPrefsKey;
            _savedValues = LoadSavedValues();
        }

        private Dictionary<string, object> LoadSavedValues()
        {
            var json = EditorPrefs.GetString(_editorPrefsKey, "{}");

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

        private void SaveValues()
        {
            var json = JsonConvert.SerializeObject(_savedValues,
                new JsonSerializerSettings()
                {
                    Culture = CultureInfo.InvariantCulture
                });

            EditorPrefs.SetString(_editorPrefsKey, json);
        }

        public void SetString(string key, string value)
        {
            SetValue(key, value);
        }

        public void SetBool(string key, bool value)
        {
            SetValue(key, value);
        }

        public void SetInt(string key, int value)
        {
            SetValue(key, value);
        }

        public void SetDouble(string key, double value)
        {
            SetValue(key, value);
        }

        public string GetString(string key, string defaultValue = null)
        {
            if (TryGetValue<string>(key, out var value))
                return value;

            return defaultValue;
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            if (TryGetValue<bool>(key, out var value))
                return value;

            return defaultValue;
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            if (TryGetValue<int>(key, out var value))
                return value;

            return defaultValue;
        }

        public double GetDouble(string key, double defaultValue = 0.0f)
        {
            if (TryGetValue<double>(key, out var value))
                return value;

            return defaultValue;
        }

        public void DeleteKey(string key)
        {
            if (_savedValues.ContainsKey(key))
            {
                _savedValues.Remove(key);
                SaveValues();
            }
        }

        public void DeleteAll()
        {
            _savedValues.Clear();
            SaveValues();
        }

        private void SetValue(string key, object value)
        {
            _savedValues[key] = value;

            SaveValues();
        }

        private bool TryGetValue<T>(string key, out T value)
        {
            value = default;

            if (!_savedValues.ContainsKey(key))
                return false;

            value = (T)_savedValues[key];

            return true;
        }
    }
}
