using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class IntegerFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 80;

        private IntegerField _integerField;
        private VisualElement _inputElement;

        private float _previousLabelMinWidth;

        public IntegerFieldRecommendedStyle(IntegerField integerField)
        {
            _integerField = integerField;
            _inputElement = _integerField[1];
        }

        protected override void ApplyInsideGroupStyle()
        {
            _integerField.labelElement.style.minWidth = _previousLabelMinWidth;
            _inputElement.style.minWidth = Length.Auto();
            _inputElement.style.overflow = StyleKeyword.Null;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousLabelMinWidth = _integerField.labelElement.style.minWidth.value.value;

            _integerField.labelElement.style.minWidth = Length.Auto();
            _inputElement.style.minWidth = MIN_WIDTH;
            _inputElement.style.overflow = Overflow.Visible;
        }
    }
}