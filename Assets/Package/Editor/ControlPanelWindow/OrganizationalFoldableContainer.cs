using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class OrganizationalFoldableContainer : Box
    {
        private static readonly Color ORGANIZATIONAL_FOLDABLE_CONTAINER_BORDER_COLOR = new Color(153f / 255f, 153f / 255f, 153f / 255f);
        private const string FOLDOUT_STATE_SAVE_KEY_BASE = "organizational-foldable-container:foldout-state:";

        private Foldout _foldout;
        private string _id;

        public OrganizationalFoldableContainer(string containerId, string foldoutText)
        {
            _id = containerId;
            name = containerId;
            style.borderTopColor = ORGANIZATIONAL_FOLDABLE_CONTAINER_BORDER_COLOR;
            style.borderTopWidth = 1;

            _foldout = new Foldout() { text = foldoutText };
            _foldout.value = GetSavedFoldoutState();
            _foldout.RegisterCallback<ChangeEvent<bool>>(SaveFoldoutState);

            Add(_foldout);
        }

        private string GetFullFoldoutStateSaveKey() => FOLDOUT_STATE_SAVE_KEY_BASE + _id;

        private void SaveFoldoutState(ChangeEvent<bool> eventArgs)
        {
            JsonEditorPrefs.SetBool(GetFullFoldoutStateSaveKey(), eventArgs.newValue);
        }

        private bool GetSavedFoldoutState()
        {
            return JsonEditorPrefs.GetBool(GetFullFoldoutStateSaveKey(), false);
        }

        public void SetControllers(IEnumerable<MainToolbarElementController> controllers)
        {
            _foldout.Clear();

            foreach (var controller in controllers)
            {
                _foldout.Add(controller);
            }
        }
    }
}