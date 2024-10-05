using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ObjectFieldRecommendedStyle : RecommendedStyle
    {
        private ObjectField _objectField;

        public ObjectFieldRecommendedStyle(ObjectField objectField)
        {
            _objectField = objectField;
        }

        protected override void ApplyRootElementStyle()
        {
            _objectField.labelElement.style.minWidth = Length.Auto();

            var inputFieldIndex = 1;

            if (string.IsNullOrEmpty(_objectField.label))
                inputFieldIndex = 0;

            var inputElement = _objectField[inputFieldIndex];
            inputElement.style.overflow = Overflow.Visible;
        }
    }
}