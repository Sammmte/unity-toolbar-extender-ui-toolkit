using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class IMGUIFloatFieldRecommendedStyle : RecommendedStyle
    {
        private readonly IMGUIFloatField _floatField;
        private float _previousFieldWidth;
        private StyleLength _previousMarginRight;

        public IMGUIFloatFieldRecommendedStyle(IMGUIFloatField floatField)
        {
            _floatField = floatField;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _floatField.FieldWidth = _previousFieldWidth;
            _floatField.style.marginRight = _previousMarginRight;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousFieldWidth = _floatField.FieldWidth;
            _previousMarginRight = _floatField.style.marginRight;

            _floatField.FieldWidth = 50;
            _floatField.style.marginRight = 3;
        }
    }
}