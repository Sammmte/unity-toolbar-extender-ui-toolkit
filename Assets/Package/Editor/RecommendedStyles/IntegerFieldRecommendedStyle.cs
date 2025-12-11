using UnityEngine;
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
            var inputFieldIndex = 1;

            if(string.IsNullOrEmpty(_integerField.label))
                inputFieldIndex = 0;

            var inputElement = _integerField[inputFieldIndex];

            _integerField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }

        protected override void ApplyInsideGroupStyle()
        {
            var inputFieldIndex = 1;

            if(string.IsNullOrEmpty(_integerField.label))
                inputFieldIndex = 0;

            var inputElement = _integerField[inputFieldIndex];

            _integerField.labelElement.style.minWidth = Length.Auto();
            inputElement.style.minWidth = MIN_WIDTH;
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}