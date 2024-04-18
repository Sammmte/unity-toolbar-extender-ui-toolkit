using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
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
            _dropdown.style.overflow = Overflow.Visible;
            _dropdown.RemoveFromClassList("unity-toolbar-button");
            _dropdown.AddToClassList("unity-button");
            _arrow.AddToClassList("unity-base-popup-field__arrow");
        }

        protected override void ApplyRootElementStyle()
        {
            _dropdown.style.flexDirection = FlexDirection.Row;
            _dropdown.style.flexWrap = Wrap.NoWrap;
            _dropdown.style.overflow = StyleKeyword.Null;
            _dropdown.RemoveFromClassList("unity-button");
            _dropdown.AddToClassList("unity-toolbar-button");
            _arrow.RemoveFromClassList("unity-base-popup-field__arrow");
        }
    }
}