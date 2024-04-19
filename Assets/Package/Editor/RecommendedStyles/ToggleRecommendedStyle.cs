using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ToggleRecommendedStyle : RecommendedStyle
    {
        private Toggle _toggle;

        public ToggleRecommendedStyle(Toggle toggle)
        {
            _toggle = toggle;
        }

        protected override void ApplyRootElementStyle()
        {
            _toggle.labelElement.style.minWidth = 0;
        }
    }
}