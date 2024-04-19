using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class SliderRecommendedStyle : RecommendedStyle
    {
        private const float MIN_WIDTH = 200;

        private readonly Slider _slider;

        public SliderRecommendedStyle(Slider slider)
        {
            _slider = slider;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _slider.style.flexWrap = Wrap.NoWrap;
            _slider.style.flexShrink = 1;
        }

        protected override void ApplyRootElementStyle()
        {
            _slider.labelElement.style.minWidth = 0;
            _slider.style.minWidth = MIN_WIDTH;
            _slider.style.flexShrink = 0;
        }
    }
}