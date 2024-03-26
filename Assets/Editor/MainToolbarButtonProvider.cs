using System;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarButtonProvider : SimpleMainToolbarElementProvider
    {
        private Action _onClick;
        private string _label;

        public MainToolbarButtonProvider(string label, Action onClick)
        {
            _label = label;
            _onClick = onClick;
        }

        protected override VisualElement BuildElement(bool isGrouped)
        {
            if(isGrouped)
            {
                var button = new Button(_onClick);
                button.text = _label;

                return button;
            }
            else
            {
                return new EditorToolbarButton(_label, _onClick);
            }
        }
    }
}