using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class JsonEditorPrefsMainToolbarElementOverrideRepository : IMainToolbarElementOverrideRepository
    {
        private struct SerializableOverride
        {
            public string ElementId;
            public bool Visible;
        }

        private const string SAVE_KEY = ToolInfo.EDITOR_PREFS_BASE_SAVE_KEY + "main-toolbar-element-overrides";

        private Dictionary<string, MainToolbarElementOverride> _overrides = new Dictionary<string, MainToolbarElementOverride>();

        public JsonEditorPrefsMainToolbarElementOverrideRepository()
        {
            DeleteSave();
            _overrides = LoadOverrides();
        }

        public void Clear()
        {
            _overrides.Clear();
        }

        public MainToolbarElementOverride? Get(string elementId)
        {
            if(_overrides.ContainsKey(elementId))
                return _overrides[elementId];

            return null;
        }

        public MainToolbarElementOverride[] GetAll()
        {
            return _overrides.Values.ToArray();
        }

        public void Save(MainToolbarElementOverride elementOverride)
        {
            _overrides[elementOverride.ElementId] = elementOverride;
            SaveOverrides();
        }

        private MainToolbarElementOverride FromSerialized(SerializableOverride serializableOverride)
        {
            return new MainToolbarElementOverride(serializableOverride.ElementId, serializableOverride.Visible);
        }

        private SerializableOverride ToSerialized(MainToolbarElementOverride mainToolbarElementOverride)
        {
            return new SerializableOverride() { 
                ElementId = mainToolbarElementOverride.ElementId, 
                Visible = mainToolbarElementOverride.Visible 
            };
        }

        private void DeleteSave()
        {
            EditorPrefs.DeleteKey(SAVE_KEY);
        }

        private Dictionary<string, MainToolbarElementOverride> LoadOverrides()
        {
            var json = EditorPrefs.GetString(SAVE_KEY, "{}");

            var serializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, SerializableOverride>>(json);

            return serializedDictionary.Values
                .ToDictionary(serializedOverride => serializedOverride.ElementId,
                serializedOverride => FromSerialized(serializedOverride));
        }

        private void SaveOverrides()
        {
            var serializableDictionary = _overrides.Values.ToDictionary(
                mainToolbarElementOverride => mainToolbarElementOverride.ElementId,
                mainToolbarElementOverride => ToSerialized(mainToolbarElementOverride)
                );

            var json = JsonConvert.SerializeObject(serializableDictionary);

            EditorPrefs.SetString(SAVE_KEY, json);
        }
    }
}