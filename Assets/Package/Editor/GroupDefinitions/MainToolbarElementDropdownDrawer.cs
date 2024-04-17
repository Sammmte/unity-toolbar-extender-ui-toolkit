using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CustomPropertyDrawer(typeof(MainToolbarElementDropdownAttribute))]
    internal class MainToolbarElementDropdownDrawer : PropertyDrawer
    {
        private string _groupId;
        private IEnumerable<string> _allIds;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _groupId = property.serializedObject.FindProperty("_groupId").stringValue;

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
            _allIds = MainToolbarAutomaticExtender.CustomMainToolbarElements.Select(el => el.Id)
                .Concat(MainToolbarAutomaticExtender.GroupElements.Select(g => g.Id));
        }

        private List<string> GetAvailableIds()
        {
            return ScriptableGroupDefinitionHelper.GetUnusedIds(_allIds)
                .Where(id => id != _groupId)
                .Where(id => !ScriptableGroupDefinitionHelper.FirstGroupIsParentOfSecond(id, _groupId))
                .OrderBy(id => id)
                .ToList();
        }
    }
}
