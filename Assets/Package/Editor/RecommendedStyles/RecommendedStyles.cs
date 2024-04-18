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
                visualElement.UnregisterCallback<AttachToPanelEvent>(ApplyRecommendedStyleAsRoot);
                visualElement.UnregisterCallback<AttachToPanelEvent>(ApplyRecommendedStyleOnGroup);

                if (isInsideGroup)
                    visualElement.RegisterCallback<AttachToPanelEvent>(ApplyRecommendedStyleOnGroup);
                else
                    visualElement.RegisterCallback<AttachToPanelEvent>(ApplyRecommendedStyleAsRoot);
            }
        }

        private static void ApplyRecommendedStyleOnGroup(AttachToPanelEvent eventArgs)
        {
            var visualElement = eventArgs.target as VisualElement;

            visualElement.UnregisterCallback<AttachToPanelEvent>(ApplyRecommendedStyleOnGroup);

            _recommendedStyles[visualElement].Apply(true);
        }

        private static void ApplyRecommendedStyleAsRoot(AttachToPanelEvent eventArgs)
        {
            var visualElement = eventArgs.target as VisualElement;

            visualElement.UnregisterCallback<AttachToPanelEvent>(ApplyRecommendedStyleAsRoot);

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
            else if (visualElement is EditorToolbarDropdown dropdown)
                return new EditorToolbarDropdownRecommendedStyle(dropdown);
            else if (visualElement is IntegerField integerField)
                return new IntegerFieldRecommendedStyle(integerField);
            else if (visualElement is FloatField floatField)
                return new FloatFieldRecommendedStyle(floatField);
            else if (visualElement is TextField textField)
                return new TextFieldRecommendedStyle(textField);

            return null;
        }
    }
}