using System.Reflection;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ButtonRecommendedStyle : RecommendedStyle
    {
        private const string DEFAULT_BUTTON_CLASS = "unity-button";
        private const string BUTTON_CLASS_FOR_TOOLBAR = "unity-toolbar-button";

        private Button _button;

        public ButtonRecommendedStyle(Button button)
        {
            _button = button;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _button.RemoveFromClassList(BUTTON_CLASS_FOR_TOOLBAR);
            _button.AddToClassList(DEFAULT_BUTTON_CLASS);
        }

        protected override void ApplyRootElementStyle()
        {
            _button.RemoveFromClassList(DEFAULT_BUTTON_CLASS);
            _button.AddToClassList(BUTTON_CLASS_FOR_TOOLBAR);
        }
    }

    internal class EditorToolbarDropdownRecommendedStyle : RecommendedStyle
    {
        private EditorToolbarDropdown _dropdown;
        private VisualElement _arrow;

        public EditorToolbarDropdownRecommendedStyle(EditorToolbarDropdown dropdown)
        {
            _dropdown = dropdown;
            _arrow = _dropdown[2];
            ApplyGroupStyleAlways = true;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _dropdown.style.flexDirection = FlexDirection.Row;
            _dropdown.style.flexWrap = Wrap.NoWrap;
            _arrow.AddToClassList("unity-base-popup-field__arrow");
        }

        protected override void ApplyRootElementStyle()
        {
            _dropdown.style.flexDirection = FlexDirection.Row;
            _dropdown.style.flexWrap = Wrap.Wrap;
            _arrow.RemoveFromClassList("unity-base-popup-field__arrow");
        }
    }
}