using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [CustomPropertyDrawer(typeof(ParentGroupIdDropdownAttribute))]
    internal class ParentGroupIdDropdownPropertyDrawer : PropertyDrawer
    {
        private string _groupId;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _groupId = property.serializedObject.FindProperty("_groupId").stringValue;

            var groupIds = ScriptableGroupDefinitionHelper.GetGroupIds();

            var options = GetOptions();

            var popupField = new PopupField<string>("Parent Group Id", options, 0);
            popupField.AddToClassList("unity-base-field__aligned");

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = popupField.value;
                property.serializedObject.ApplyModifiedProperties();
            }
            else if(groupIds.Contains(property.stringValue))
            {
                popupField.SetValueWithoutNotify(property.stringValue);
            }

            popupField.RegisterCallback<ChangeEvent<string>>(ev =>
            {
                property.stringValue = ev.newValue;
                property.serializedObject.ApplyModifiedProperties();
                ScriptableGroupDefinitionHelper.Refresh();
                popupField.choices = GetOptions();
            });

            return popupField;
        }

        private List<string> GetOptions()
        {
            var availableIds = ScriptableGroupDefinitionHelper.GetUnusedGroupIds()
                .Where(id => id != _groupId);

            var options = availableIds
                .ToList();

            options.Insert(0, ScriptableGroupDefinitionHelper.NO_PARENT_VALUE);

            return options;
        }
    }
}
