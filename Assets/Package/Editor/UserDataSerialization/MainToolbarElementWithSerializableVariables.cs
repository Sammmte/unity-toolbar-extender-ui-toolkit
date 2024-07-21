using System.Linq;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementWithSerializableVariables
    {
        public MainToolbarElement MainToolbarElement { get; }
        public int VariableCount => _serializableFields.Length;

        private FieldInfo[] _serializableFields = new FieldInfo[0];

        public MainToolbarElementWithSerializableVariables(MainToolbarElement mainToolbarElement)
        {
            MainToolbarElement = mainToolbarElement;

            LoadSerializableVariables();
        }

        private void LoadSerializableVariables()
        {
            var mainToolbarElementInnerType = MainToolbarElement.VisualElement.GetType();

            _serializableFields = mainToolbarElementInnerType.GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => field.GetCustomAttribute<SerializeAttribute>(false) != null)
                .ToArray();
        }

        public void LoadFromSerializedData(MainToolbarElementSerializedData serializedData)
        {
            foreach(var field in _serializableFields)
            {
                var keyValue = serializedData.KeyValues.FirstOrDefault(keyValue => keyValue.Key == field.Name);

                if (string.IsNullOrEmpty(keyValue.Key))
                    continue;

                field.SetValue(MainToolbarElement.VisualElement, keyValue.Value);
            }
        }
    }
}
