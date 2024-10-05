using System.Linq;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class Vector2FieldRecommendedStyle : RecommendedStyle
    {
        private const int SINGLE_FIELD_MIN_WIDTH = 80;

        private readonly Vector2Field _vector2Field;

        public Vector2FieldRecommendedStyle(Vector2Field vector2Field)
        {
            _vector2Field = vector2Field;
        }

        protected override void ApplyRootElementStyle()
        {
            _vector2Field.labelElement.style.minWidth = Length.Auto();

            var inputFieldsParentElement = GetInputFieldsParentElement();

            foreach (var child in inputFieldsParentElement.Children().Where(childElement => childElement is FloatField))
            {
                var inputFieldElementIndex = 1;

                var inputFieldElement = child[inputFieldElementIndex];

                inputFieldElement.style.overflow = Overflow.Visible;
                inputFieldElement.style.minWidth = SINGLE_FIELD_MIN_WIDTH;
            }

            var spacerElement = inputFieldsParentElement.Children().Last();

            spacerElement.style.flexGrow = 0;
        }

        protected override void ApplyInsideGroupStyle()
        {
            var inputFieldsParentElement = GetInputFieldsParentElement();

            inputFieldsParentElement.style.flexWrap = Wrap.Wrap;

            var spacerElement = inputFieldsParentElement.Children().Last();

            spacerElement.style.flexGrow = 0;
        }

        private VisualElement GetInputFieldsParentElement()
        {
            var inputFieldsParentIndex = 1;

            if (string.IsNullOrEmpty(_vector2Field.label))
                inputFieldsParentIndex = 0;

            return _vector2Field[inputFieldsParentIndex];
        }
    }
}