using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
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

        protected override void ApplyInsideGroupStyle()
        {
            _button.style.overflow = StyleKeyword.Null;
        }

        protected override void ApplyRootElementStyle()
        {
            _button.style.overflow = Overflow.Visible;
        }
    }
}