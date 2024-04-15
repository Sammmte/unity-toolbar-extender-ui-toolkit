using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class IMGUITextFieldRecommendedStyle : RecommendedStyle
    {
        private readonly IMGUITextField _textField;
        private float _previousFieldWidth;
        private StyleLength _previousMarginRight;

        public IMGUITextFieldRecommendedStyle(IMGUITextField textField)
        {
            _textField = textField;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _textField.FieldWidth = _previousFieldWidth;
            _textField.style.marginRight = _previousMarginRight;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousFieldWidth = _textField.FieldWidth;
            _previousMarginRight = _textField.style.marginRight;

            _textField.FieldWidth = 100;
            _textField.style.marginRight = 3;
        }
    }
}