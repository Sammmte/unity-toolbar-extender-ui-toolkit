using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class TextFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 120;

        private TextField _textField;
        private VisualElement _inputElement;

        private float _previousLabelMinWidth;

        public TextFieldRecommendedStyle(TextField textField)
        {
            _textField = textField;
            _inputElement = _textField[1];
        }

        protected override void ApplyInsideGroupStyle()
        {
            _textField.labelElement.style.minWidth = _previousLabelMinWidth;
            _inputElement.style.minWidth = Length.Auto();
            _inputElement.style.overflow = StyleKeyword.Null;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousLabelMinWidth = _textField.labelElement.style.minWidth.value.value;

            _textField.labelElement.style.minWidth = Length.Auto();
            _inputElement.style.minWidth = MIN_WIDTH;
            _inputElement.style.overflow = Overflow.Visible;
        }
    }
}