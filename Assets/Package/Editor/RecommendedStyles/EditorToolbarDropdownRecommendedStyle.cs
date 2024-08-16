﻿using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class EditorToolbarDropdownRecommendedStyle : RecommendedStyle
    {
        private EditorToolbarDropdown _dropdown;

        public EditorToolbarDropdownRecommendedStyle(EditorToolbarDropdown dropdown)
        {
            _dropdown = dropdown;
        }

        protected override void ApplyInsideGroupStyle()
        {
            var arrowIndex = 2;

            if (string.IsNullOrEmpty(_dropdown.text))
                arrowIndex = 1;

            var arrow = _dropdown[arrowIndex];

            _dropdown.style.flexDirection = FlexDirection.Row;
            _dropdown.style.flexWrap = Wrap.NoWrap;
            _dropdown.style.overflow = Overflow.Visible;
            _dropdown.RemoveFromClassList("unity-toolbar-button");
            _dropdown.AddToClassList("unity-button");
            arrow.AddToClassList("unity-base-popup-field__arrow");
        }

        protected override void ApplyRootElementStyle()
        {
            var arrowIndex = 2;

            if (string.IsNullOrEmpty(_dropdown.text))
                arrowIndex = 1;

            var arrow = _dropdown[arrowIndex];

            _dropdown.style.flexDirection = FlexDirection.Row;
            _dropdown.style.flexWrap = Wrap.NoWrap;
            _dropdown.style.overflow = StyleKeyword.Null;
            _dropdown.RemoveFromClassList("unity-button");
            _dropdown.AddToClassList("unity-toolbar-button");
            arrow.RemoveFromClassList("unity-base-popup-field__arrow");
        }
    }
}