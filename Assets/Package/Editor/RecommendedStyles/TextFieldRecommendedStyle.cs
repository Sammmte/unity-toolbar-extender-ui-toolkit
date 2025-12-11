using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class TextFieldRecommendedStyle : RecommendedStyle
    {
        private const int MIN_WIDTH = 120;

        private TextField _textField;

        public TextFieldRecommendedStyle(TextField textField)
        {
            _textField = textField;
        }

        protected override void ApplyRootElementStyle()
        {
            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_textField.label))
                inputFieldIndex = 0;

            var inputElement = _textField[inputFieldIndex];

            _textField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }

        protected override void ApplyInsideGroupStyle()
        {
            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_textField.label))
                inputFieldIndex = 0;

            var inputElement = _textField[inputFieldIndex];

            _textField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}