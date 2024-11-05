using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class TagFieldRecommendedStyle : RecommendedStyle
    {
        private TagField _tagField;

        public TagFieldRecommendedStyle(TagField tagField)
        {
            _tagField = tagField;
        }

        protected override void ApplyRootElementStyle()
        {
            _tagField.labelElement.style.minWidth = Length.Auto();

            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_tagField.label))
                inputFieldIndex = 0;

            var inputElement = _tagField[inputFieldIndex];
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}