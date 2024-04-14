using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CustomPropertyDrawer(typeof(MainToolbarElementDropdownAttribute))]
    internal class MainToolbarElementDropdownDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var idList = MainToolbarAutomaticExtender.CustomMainToolbarElements.Select(el => el.Id)
                .OrderBy(shortName => shortName)
                .ToList();

            var popupField = new PopupField<string>(
                choices: idList,
                0);

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = popupField.value;
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                if (idList.Contains(property.stringValue))
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
