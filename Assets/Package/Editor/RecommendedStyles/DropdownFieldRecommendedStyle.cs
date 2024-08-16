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

        protected override void ApplyRootElementStyle()
        {
            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_dropdownField.label))
                inputFieldIndex = 0;

            var inputElement = _dropdownField[inputFieldIndex];
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}