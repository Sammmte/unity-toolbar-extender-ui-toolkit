using System;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarButtonProvider : SimpleMainToolbarElementProvider
    {
        private Action _onClick;
        private string _label;
        private Texture2D _icon;

        public MainToolbarButtonProvider(string label, Action onClick)
        {
            _label = label;
            _onClick = onClick;
        }

        public MainToolbarButtonProvider(Texture2D icon, Action onClick)
        {
            _icon = icon;
            _onClick = onClick;
        }

        protected override VisualElement BuildElement(bool isGrouped)
        {
            if(isGrouped)
            {
                var button = new Button(_onClick);
                
                if(_icon != null)
                    button.Add(new Image() { image = _icon });
                else
                    button.text = _label;

                return button;
            }
            else
            {
                if(_icon != null)
                    return new EditorToolbarButton(_icon, _onClick);
                else
                    return new EditorToolbarButton(_label, _onClick);
            }
        }
    }
}