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
        private SerializableElementGroup ElementGroup
        {
            get
            {
                if (_elementGroup == null)
                    _elementGroup = Load();

                return _elementGroup;
            }
            set => _elementGroup = value;
        }

        public UserSettingsFileMainToolbarElementVariableRepository(IMainToolbarElementVariableSerializer serializer)
        {
            _serializer = serializer;
        }

        public SerializableElement[] GetAll()
        {
            return ElementGroup.SerializableElements.Values.ToArray();
        }

        public void Set(SerializableElement serializableElement)
        {
            ElementGroup.SerializableElements[serializableElement.ElementFullTypeName] = serializableElement;
        }

        public void SetAll(SerializableElement[] serializableElements)
        {
            ElementGroup.SerializableElements = serializableElements.ToDictionary(s => s.ElementFullTypeName, s => s);
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
            var serializedString = _serializer.Serialize(ElementGroup);
            
            if(!Directory.Exists(DIRECTORY))
                Directory.CreateDirectory(DIRECTORY);
            
            File.WriteAllText(FILE, serializedString);
        }
    }
}
