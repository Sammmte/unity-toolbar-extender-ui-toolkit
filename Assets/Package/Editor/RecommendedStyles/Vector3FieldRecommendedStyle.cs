using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class Vector3FieldRecommendedStyle : RecommendedStyle
    {
        private const int SINGLE_FIELD_MIN_WIDTH = 80;

        private readonly Vector3Field _vector3Field;

        public Vector3FieldRecommendedStyle(Vector3Field vector3Field)
        {
            _vector3Field = vector3Field;
        }

        protected override void ApplyRootElementStyle()
        {
            _vector3Field.labelElement.style.minWidth = Length.Auto();

            var inputFieldsParentElement = GetInputFieldsParentElement();

            foreach(var floatField in inputFieldsParentElement.Children())
            {
                var inputFieldElementIndex = 1;

                var inputFieldElement = floatField[inputFieldElementIndex];

                inputFieldElement.style.overflow = Overflow.Visible;
                inputFieldElement.style.minWidth = SINGLE_FIELD_MIN_WIDTH;
            }
        }

        protected override void ApplyInsideGroupStyle()
        {
            var inputFieldsParentElement = GetInputFieldsParentElement();

            inputFieldsParentElement.style.flexWrap = Wrap.Wrap;
        }

        private VisualElement GetInputFieldsParentElement()
        {
            var inputFieldsParentIndex = 1;

            if (string.IsNullOrEmpty(_vector3Field.label))
                inputFieldsParentIndex = 0;

            return _vector3Field[inputFieldsParentIndex];
        }
    }
}