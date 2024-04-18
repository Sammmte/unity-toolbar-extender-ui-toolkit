using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class FloatFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 80;

        private FloatField _floatField;
        private VisualElement _inputElement;

        private float _previousLabelMinWidth;

        public FloatFieldRecommendedStyle(FloatField floatField)
        {
            _floatField = floatField;
            _inputElement = _floatField[1];
        }

        protected override void ApplyInsideGroupStyle()
        {
            _floatField.labelElement.style.minWidth = _previousLabelMinWidth;
            _inputElement.style.minWidth = Length.Auto();
            _inputElement.style.overflow = StyleKeyword.Null;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousLabelMinWidth = _floatField.labelElement.style.minWidth.value.value;

            _floatField.labelElement.style.minWidth = Length.Auto();
            _inputElement.style.minWidth = MIN_WIDTH;
            _inputElement.style.overflow = Overflow.Visible;
        }
    }
}