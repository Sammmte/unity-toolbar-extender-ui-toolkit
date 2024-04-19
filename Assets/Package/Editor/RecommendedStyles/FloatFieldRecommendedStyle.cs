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
            var inputElement = _floatField[1];

            _floatField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}