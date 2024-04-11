using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CustomPropertyDrawer(typeof(MainToolbarElementTypeDropdownAttribute))]
    internal class MainToolbarElementTypeDropdownDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var typesList = ServicesAndRepositories.MainToolbarElementRepository
                .GetAll()
                .Select(mainToolbarElement => mainToolbarElement.VisualElement.GetType().Name)
                .ToList();

            var popupField = new PopupField<string>(
                choices: typesList,
                0);

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = popupField.value;
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                if (typesList.Contains(property.stringValue))
                    popupField.SetValueWithoutNotify(property.stringValue);
                else
                    popupField.SetValueWithoutNotify("");
            }

            popupField.RegisterCallback<ChangeEvent<string>>(ev =>
            {
                property.stringValue = ev.newValue;
                property.serializedObject.ApplyModifiedProperties();
            });

            return popupField;
        }
    }
}
