using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class EnumFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 80;
        private EnumField _enumField;

        public EnumFieldRecommendedStyle(EnumField enumField)
        {
            _enumField = enumField;
        }

        protected override void ApplyRootElementStyle() 
        {
            _enumField.labelElement.style.minWidth = Length.Auto();

            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_enumField.label))
                inputFieldIndex = 0;

            var inputElement = _enumField[inputFieldIndex];
            inputElement.style.overflow = Overflow.Visible;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _enumField.labelElement.style.minWidth = Length.Auto();

            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_enumField.label))
                inputFieldIndex = 0;

            var inputElement = _enumField[inputFieldIndex];
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}