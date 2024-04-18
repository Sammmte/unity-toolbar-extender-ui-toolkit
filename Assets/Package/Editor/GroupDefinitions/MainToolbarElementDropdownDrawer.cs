using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CustomPropertyDrawer(typeof(MainToolbarElementDropdownAttribute))]
    internal class MainToolbarElementDropdownDrawer : PropertyDrawer
    {
        private string _groupId;
        private IEnumerable<string> _mainToolbarElementsIds;
        private IEnumerable<string> _groupIds;
        private IEnumerable<string> _allIds;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _groupId = property.serializedObject.FindProperty("_groupId").stringValue;

            CacheAllIds();

            var availableIds = GetAvailableIds();

            var popupField = new PopupField<string>(
                choices: availableIds,
                0);

            Restore(property, availableIds, popupField);

            popupField.RegisterCallback<ChangeEvent<string>>(ev =>
            {
                property.stringValue = ev.newValue;
                property.serializedObject.ApplyModifiedProperties();
                ScriptableGroupDefinitionHelper.Refresh();
                CacheAllIds();
                var newAvailableIds = GetAvailableIds();
                newAvailableIds.Add(property.stringValue);
                popupField.choices = newAvailableIds.OrderBy(id => id).ToList();
            });

            return popupField;
        }

        private void Restore(SerializedProperty property, List<string> availableIds, PopupField<string> popupField)
        {
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = popupField.value;
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                if (_allIds.Contains(property.stringValue))
                {
                    availableIds.Add(property.stringValue);
                    popupField.choices = availableIds.OrderBy(id => id).ToList();
                    popupField.SetValueWithoutNotify(property.stringValue);
                }
                else
                    popupField.SetValueWithoutNotify("");
            }
        }

        private void CacheAllIds()
        {
            _mainToolbarElementsIds = MainToolbarAutomaticExtender.CustomMainToolbarElements.Select(el => el.Id);
            _groupIds = ScriptableGroupDefinitionHelper.GetGroupIds();

            _allIds = _mainToolbarElementsIds
                .Concat(_groupIds);
        }

        private List<string> GetAvailableIds()
        {
            var unusedMainToolbarElementIds = ScriptableGroupDefinitionHelper.GetUnusedMainToolbarElementIds(_mainToolbarElementsIds);
            var unusedGroupIds = ScriptableGroupDefinitionHelper.GetEligibleGroupChildsFor(_groupId);

            return unusedMainToolbarElementIds
                .Concat(unusedGroupIds)
                .OrderBy(id => id)
                .ToList();
        }
    }
}
