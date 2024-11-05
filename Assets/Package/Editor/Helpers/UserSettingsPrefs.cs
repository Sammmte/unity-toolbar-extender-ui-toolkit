using System;
using System.Collections.Generic;
using System.IO;
using Unity.Serialization.Json;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public static class UserSettingsPrefs
    {
        private static readonly string DIRECTORY = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "UserSettings/", "unity-toolbar-extender-ui-toolkit", "user-settings-prefs");
        private static readonly string FILE = Path.Combine(DIRECTORY, "user-settings-prefs.json");

        private static Dictionary<string, object> _prefs;
        private static Dictionary<string, object> Prefs
        {
            get
            {
                if (_prefs == null)
                    _prefs = Load();

                return _prefs;
            }
        }

        public static void SetInt(string key, int value)
        {
            Prefs[key] = value;
            Save();
        }

        public static void SetFloat(string key, float value)
        {
            Prefs[key] = value;
            Save();
        }

        public static void SetDouble(string key, double value)
        {
            Prefs[key] = value;
            Save();
        }

        public static void SetBool(string key, bool value)
        {
            Prefs[key] = value;
            Save();
        }

        public static void SetString(string key, string value)
        {
            Prefs[key] = value;
            Save();
        }

        public static string GetString(string key, string defaultValue = null)
        {
            if (Prefs.ContainsKey(key))
                return (string)Prefs[key];

            return defaultValue;
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            if (Prefs.ContainsKey(key))
                return (bool)Prefs[key];

            return defaultValue;
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            if (Prefs.ContainsKey(key))
                return Convert.ToInt32(Prefs[key]);

            return defaultValue;
        }

        public static float GetFloat(string key, float defaultValue = 0.0f)
        {
            if (Prefs.ContainsKey(key))
                return Convert.ToSingle(Prefs[key]);

            return defaultValue;
        }

        public static double GetDouble(string key, double defaultValue = 0.0)
        {
            if (Prefs.ContainsKey(key))
                return Convert.ToDouble(Prefs[key]);

            return defaultValue;
        }

        private static Dictionary<string, object> Load()
        {
            if (!Directory.Exists(DIRECTORY))
                Directory.CreateDirectory(DIRECTORY);

            if (!File.Exists(FILE))
                return JsonSerialization.FromJson<Dictionary<string, object>>("{}");

            var json = File.ReadAllText(FILE);

            var serializedDictionary = JsonSerialization.FromJson<Dictionary<string, object>>(json);

            return serializedDictionary;
        }

        private static void Save()
        {
            var json = JsonSerialization.ToJson(Prefs);

            File.WriteAllText(FILE, json);
        }
    }
}
