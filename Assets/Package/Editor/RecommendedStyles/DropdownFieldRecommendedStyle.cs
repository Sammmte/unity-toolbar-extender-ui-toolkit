using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class DropdownFieldRecommendedStyle : RecommendedStyle
    {
        private DropdownField _dropdownField;

        public DropdownFieldRecommendedStyle(DropdownField dropdownField)
        {
            _dropdownField = dropdownField;
        }

        protected override void ApplyInsideGroupStyle()
        {
            var inputElement = _dropdownField[1];
            inputElement.style.overflow = StyleKeyword.Null;
        }

        protected override void ApplyRootElementStyle()
        {
            var inputElement = _dropdownField[1];
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}