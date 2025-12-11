using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class DropdownFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 80;
        private DropdownField _dropdownField;

        public DropdownFieldRecommendedStyle(DropdownField dropdownField)
        {
            _dropdownField = dropdownField;
        }

        protected override void ApplyRootElementStyle()
        {
            _dropdownField.labelElement.style.minWidth = Length.Auto();

            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_dropdownField.label))
                inputFieldIndex = 0;

            var inputElement = _dropdownField[inputFieldIndex];
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}