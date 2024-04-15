using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class IMGUIIntFieldRecommendedStyle : RecommendedStyle
    {
        private readonly IMGUIIntField _intField;
        private float _previousFieldWidth;
        private StyleLength _previousMarginRight;

        public IMGUIIntFieldRecommendedStyle(IMGUIIntField intField)
        {
            _intField = intField;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _intField.FieldWidth = _previousFieldWidth;
            _intField.style.marginRight = _previousMarginRight;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousFieldWidth = _intField.FieldWidth;
            _previousMarginRight = _intField.style.marginRight;

            _intField.FieldWidth = 50;
            _intField.style.marginRight = 3;
        }
    }
}