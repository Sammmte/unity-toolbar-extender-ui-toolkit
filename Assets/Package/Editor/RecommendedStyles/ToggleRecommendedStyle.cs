using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ToggleRecommendedStyle : RecommendedStyle
    {
        private Toggle _toggle;
        private StyleLength _previousLabelMinWidth;

        public ToggleRecommendedStyle(Toggle toggle)
        {
            _toggle = toggle;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _toggle.labelElement.style.minWidth = _previousLabelMinWidth;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousLabelMinWidth = _toggle.labelElement.style.minWidth;
            _toggle.labelElement.style.minWidth = 0;
        }
    }
}