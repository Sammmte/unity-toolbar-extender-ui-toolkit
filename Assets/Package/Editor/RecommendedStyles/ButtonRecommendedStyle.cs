using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ButtonRecommendedStyle : RecommendedStyle
    {
        private Button _button;

        public ButtonRecommendedStyle(Button button)
        {
            _button = button;
        }

        protected override void ApplyRootElementStyle()
        {
            _button.style.overflow = Overflow.Visible;
        }
    }
}