using UnityEditor.Toolbars;
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
            _arrow = new VisualElement();
            _arrow.AddToClassList("unity-base-popup-field__arrow");
        }

        protected override void ApplyInsideGroupStyle()
        {
            _dropdown.RemoveFromClassList("unity-toolbar-button");
            _dropdown.AddToClassList("unity-button");
            _dropdown.style.flexDirection = FlexDirection.Row;
            if (!_dropdown.Contains(_arrow))
            {
                _dropdown.Add(_arrow);
            }
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