using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class SliderRecommendedStyle : RecommendedStyle
    {
        private const float MIN_WIDTH = 200;

        private readonly Slider _slider;
        private StyleLength _previousMinWidth;
        private StyleLength _previousLabelMinWidth;

        public SliderRecommendedStyle(Slider slider)
        {
            _slider = slider;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _slider.labelElement.style.minWidth = _previousLabelMinWidth;
            _slider.style.minWidth = _previousMinWidth;
        }

        protected override void ApplyRootElementStyle()
        {
            _previousLabelMinWidth = _slider.labelElement.style.minWidth;
            _previousMinWidth = _slider.style.minWidth;

            _slider.labelElement.style.minWidth = 0;
            _slider.style.minWidth = MIN_WIDTH;
        }
    }
}