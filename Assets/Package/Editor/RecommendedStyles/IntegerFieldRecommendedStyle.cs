using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class IntegerFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 80;

        private IntegerField _integerField;

        public IntegerFieldRecommendedStyle(IntegerField integerField)
        {
            _integerField = integerField;
        }

        protected override void ApplyRootElementStyle()
        {
            var inputElement = _integerField[1];

            _integerField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}