using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class EditorPrefsMainToolbarElementVariableRepository : IMainToolbarElementVariableRepository
    {
        private const string SAVE_KEY = "unity-toolbar-extender-ui-toolkit:user-serialized-values";
        private readonly IMainToolbarElementVariableSerializer _serializer;
        private SerializableElementGroup _elementGroup;

        public EditorPrefsMainToolbarElementVariableRepository(IMainToolbarElementVariableSerializer serializer)
        {
            _serializer = serializer;

            _elementGroup = Load();
        }

        public SerializableElement? Get(MainToolbarElement element)
        {
            var fullTypeName = element.GetType().FullName;

            if(_elementGroup.SerializableElements.ContainsKey(fullTypeName))
                return _elementGroup.SerializableElements[fullTypeName];

            return null;
        }

        public void Set(SerializableElement serializableElement)
        {
            _elementGroup.SerializableElements[serializableElement.GetType().FullName] = serializableElement;
        }

        private SerializableElementGroup Load()
        {
            var serializedString = EditorPrefs.GetString(SAVE_KEY);

            return _serializer.Deserialize(serializedString);
        }

        public void Save()
        {
            var serializedString = _serializer.Serialize(_elementGroup);

            EditorPrefs.SetString(SAVE_KEY, serializedString);
        }
    }
}
