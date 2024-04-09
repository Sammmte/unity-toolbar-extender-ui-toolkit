using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Globalization;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class JsonEditorPrefs
    {
        private const string EDITOR_PREFS_SAVE_KEY = "unity-toolbar-extender-ui-toolkit-editor-prefs";

        private static Dictionary<string, object> _savedValues;

        static JsonEditorPrefs()
        {
            _savedValues = LoadSavedValues();
        }

        private static Dictionary<string, object> LoadSavedValues()
        {
            var json = EditorPrefs.GetString(EDITOR_PREFS_SAVE_KEY, "{}");

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

        private static void SaveValues()
        {
            var json = JsonConvert.SerializeObject(_savedValues,
                new JsonSerializerSettings()
                {
                    Culture = CultureInfo.InvariantCulture
                });

            EditorPrefs.SetString(EDITOR_PREFS_SAVE_KEY, json);
        }

        public static void SetString(string key, string value)
        {
            SetValue(key, value);
        }

        public static void SetBool(string key, bool value)
        {
            SetValue(key, value);
        }

        public static void SetInt(string key, int value)
        {
            SetValue(key, value);
        }

        public static void SetDouble(string key, double value)
        {
            SetValue(key, value);
        }

        public static string GetString(string key, string defaultValue = null)
        {
            if (TryGetValue<string>(key, out var value))
                return value;

            return defaultValue;
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            if (TryGetValue<bool>(key, out var value))
                return value;

            return defaultValue;
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            if (TryGetValue<int>(key, out var value))
                return value;

            return defaultValue;
        }

        public static double GetDouble(string key, double defaultValue = 0.0f)
        {
            if (TryGetValue<double>(key, out var value))
                return value;

            return defaultValue;
        }

        public static void DeleteKey(string key)
        {
            if(_savedValues.ContainsKey(key))
            {
                _savedValues.Remove(key);
                SaveValues();
            }
        }

        public static void DeleteAll()
        {
            _savedValues.Clear();
            SaveValues();
        }

        private static void SetValue(string key, object value)
        {
            _savedValues[key] = value;

            SaveValues();
        }

        private static bool TryGetValue<T>(string key, out T value)
        {
            value = default;

            if (!_savedValues.ContainsKey(key))
                return false;

            value = (T)_savedValues[key];

            return true;
        }
    }
}
