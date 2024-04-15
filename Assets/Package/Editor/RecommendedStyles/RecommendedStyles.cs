using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class RecommendedStyles
    {
        private static Dictionary<VisualElement, RecommendedStyle> _recommendedStyles = new Dictionary<VisualElement, RecommendedStyle>();

        public static void Apply(VisualElement visualElement, bool isInsideGroup)
        {
            if(!_recommendedStyles.ContainsKey(visualElement))
            {
                var recommendedStyle = GetRecommendedStyleFor(visualElement);

                if (recommendedStyle != null)
                    _recommendedStyles.Add(visualElement, recommendedStyle);
            }

            if (_recommendedStyles.ContainsKey(visualElement))
            {
                if (isInsideGroup)
                    visualElement.RegisterCallback<GeometryChangedEvent>(ApplyRecommendedStyleOnGroup);
                else
                    visualElement.RegisterCallback<GeometryChangedEvent>(ApplyRecommendedStyleAsRoot);
            }
        }

        private static void ApplyRecommendedStyleOnGroup(GeometryChangedEvent eventArgs)
        {
            var visualElement = eventArgs.target as VisualElement;

            visualElement.UnregisterCallback<GeometryChangedEvent>(ApplyRecommendedStyleOnGroup);

            _recommendedStyles[visualElement].Apply(true);
        }

        private static void ApplyRecommendedStyleAsRoot(GeometryChangedEvent eventArgs)
        {
            var visualElement = eventArgs.target as VisualElement;

            visualElement.UnregisterCallback<GeometryChangedEvent>(ApplyRecommendedStyleAsRoot);

            _recommendedStyles[visualElement].Apply(false);
        }

        private static RecommendedStyle GetRecommendedStyleFor(VisualElement visualElement)
        {
            if (visualElement is not EditorToolbarToggle && visualElement is Toggle toggle)
                return new ToggleRecommendedStyle(toggle);
            else if (visualElement is not EditorToolbarButton && visualElement is Button button)
                return new ButtonRecommendedStyle(button);
            else if (visualElement is Slider slider)
                return new SliderRecommendedStyle(slider);
            else if (visualElement is DropdownField dropdownField)
                return new DropdownFieldRecommendedStyle(dropdownField);
            else if (visualElement is IMGUIFloatField floatField)
                return new IMGUIFloatFieldRecommendedStyle(floatField);
            else if (visualElement is IMGUIIntField intField)
                return new IMGUIIntFieldRecommendedStyle(intField);
            else if (visualElement is IMGUITextField textField)
                return new IMGUITextFieldRecommendedStyle(textField);

            return null;
        }
    }
}