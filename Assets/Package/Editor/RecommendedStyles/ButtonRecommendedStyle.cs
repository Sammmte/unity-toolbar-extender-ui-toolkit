using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ButtonRecommendedStyle : RecommendedStyle
    {
        private const string DEFAULT_BUTTON_CLASS = "unity-button";
        private const string BUTTON_CLASS_FOR_TOOLBAR = "unity-toolbar-button";

        private Button _button;

        public ButtonRecommendedStyle(Button button)
        {
            _button = button;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _button.RemoveFromClassList(BUTTON_CLASS_FOR_TOOLBAR);
            _button.AddToClassList(DEFAULT_BUTTON_CLASS);
        }

        protected override void ApplyRootElementStyle()
        {
            _button.RemoveFromClassList(DEFAULT_BUTTON_CLASS);
            _button.AddToClassList(BUTTON_CLASS_FOR_TOOLBAR);
        }
    }
}