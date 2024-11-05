using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class EnumFlagsFieldRecommendedStyle : RecommendedStyle
    {
        private EnumFlagsField _enumField;

        public EnumFlagsFieldRecommendedStyle(EnumFlagsField enumField)
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
            inputElement.style.minWidth = 120;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _enumField.style.flexGrow = 1;
        }
    }
}