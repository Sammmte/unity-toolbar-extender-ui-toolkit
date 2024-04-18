using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CustomPropertyDrawer(typeof(MainToolbarElementDropdownAttribute))]
    internal class MainToolbarElementDropdownDrawer : PropertyDrawer
    {
        private IEnumerable<string> _allIds;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            CacheAllIds();

            var availableIds = GetAvailableIds();


            var popupField = new PopupField<string>(
                choices: availableIds,
                0);

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = popupField.value;
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                if (_allIds.Contains(property.stringValue))
                    popupField.SetValueWithoutNotify(property.stringValue);
                else
                    popupField.SetValueWithoutNotify("");
            }

            popupField.RegisterCallback<ChangeEvent<string>>(ev =>
            {
                property.stringValue = ev.newValue;
                property.serializedObject.ApplyModifiedProperties();
                ScriptableGroupDefinitionHelper.Refresh();
                CacheAllIds();
                popupField.choices = GetAvailableIds();
            });

            return popupField;
        }

        private void CacheAllIds()
        {
            _allIds = MainToolbarAutomaticExtender.CustomMainToolbarElements.Select(el => el.Id);
        }

        private List<string> GetAvailableIds()
        {
            return ScriptableGroupDefinitionHelper.GetUnusedMainToolbarElementIds(_allIds)
                .OrderBy(id => id)
                .ToList();
        }
    }
}
