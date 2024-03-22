using System;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarButton : IMainToolbarElementProvider
    {
        private Action _onClick;
        private string _label;
        private VisualElement _finalVisualElement;

        public MainToolbarButton(string label, Action onClick)
        {
            _label = label;
            _onClick = onClick;
        }

        public VisualElement GetElement(bool isGrouped)
        {
            if(isGrouped)
            {
                var button = new Button(_onClick);
                button.text = _label;
                _finalVisualElement = button;
            }
            else
            {
                _finalVisualElement = new EditorToolbarButton(_label, _onClick);
            }

            return _finalVisualElement;
        }
    }
}