using System.Collections.Generic;
using System.Linq;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class RecommendedStyles
    {
        private static Dictionary<RecommendedStyleVisualElement, RecommendedStyle> _recommendedStyles = new Dictionary<RecommendedStyleVisualElement, RecommendedStyle>();

        public static void SetElements(RecommendedStyleVisualElement[] elements)
        {
            _recommendedStyles.Clear();

            foreach(var element in elements)
            {
                var recommendedStyle = GetRecommendedStyleFor(element.VisualElement);

                if (recommendedStyle == null)
                    continue;

                element.VisualElement.RegisterCallback<AttachToPanelEvent>(ApplyRecommendedStyle);
                _recommendedStyles.Add(element, recommendedStyle);
            }
        }

        private static void ApplyRecommendedStyle(AttachToPanelEvent eventArgs)
        {
            var visualElement = eventArgs.target as VisualElement;

            var key = _recommendedStyles.Keys.FirstOrDefault(key => key.VisualElement == visualElement);

            _recommendedStyles[key].Apply(key.IsInsideGroup);
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
            else if (visualElement is EditorToolbarToggle toolbarToggle)
                return new EditorToolbarToggleRecommendedStyle(toolbarToggle);

            return null;
        }
    }
}