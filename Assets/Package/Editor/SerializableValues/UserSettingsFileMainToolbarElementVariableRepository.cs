using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class UserSettingsFileMainToolbarElementVariableRepository : IMainToolbarElementVariableRepository
    {
        private static readonly string DIRECTORY = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "UserSettings/", "unity-toolbar-extender-ui-toolkit", "serialized-user-values");
        private static readonly string FILE = Path.Combine(DIRECTORY, "serialized_values.json");
        private readonly IMainToolbarElementVariableSerializer _serializer;
        private SerializableElementGroup _elementGroup;

        public UserSettingsFileMainToolbarElementVariableRepository(IMainToolbarElementVariableSerializer serializer)
        {
            _serializer = serializer;

            _elementGroup = Load();
        }

        public SerializableElement[] GetAll()
        {
            return _elementGroup.SerializableElements.Values.ToArray();
        }

        public void Set(SerializableElement serializableElement)
        {
            _elementGroup.SerializableElements[serializableElement.ElementFullTypeName] = serializableElement;
        }

        private SerializableElementGroup Load()
        {
            if(!Directory.Exists(DIRECTORY))
                Directory.CreateDirectory(DIRECTORY);

            if (!File.Exists(FILE))
                return _serializer.Deserialize("{}");
            else
                return _serializer.Deserialize(File.ReadAllText(FILE));
        }

        public void Save()
        {
            var serializedString = _serializer.Serialize(_elementGroup);
            Debug.Log(serializedString);
            File.WriteAllText(FILE, serializedString);
        }

        [MenuItem("Paps/Clear Serializables")]
        public static void Clear()
        {
            File.Delete(FILE);
        }

        [MenuItem("Paps/Show Serializables")]
        public static void Show()
        {
            Debug.Log(File.Exists(FILE) ? File.ReadAllText(FILE) : "{}");
        }
    }
}
