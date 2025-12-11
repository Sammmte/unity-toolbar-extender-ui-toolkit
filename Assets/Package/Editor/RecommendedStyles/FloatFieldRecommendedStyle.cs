using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class FloatFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 80;

        private FloatField _floatField;

        public FloatFieldRecommendedStyle(FloatField floatField)
        {
            _floatField = floatField;
        }

        protected override void ApplyRootElementStyle()
        {
            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_floatField.label))
                inputFieldIndex = 0;

            var inputElement = _floatField[inputFieldIndex];

            _floatField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }

        protected override void ApplyInsideGroupStyle()
        {
            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_floatField.label))
                inputFieldIndex = 0;

            var inputElement = _floatField[inputFieldIndex];

            _floatField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}