using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
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
            
            ApplySafely(key);
        }

        private static void ApplySafely(RecommendedStyleVisualElement key)
        {
            try
            {
                _recommendedStyles[key].Apply(key.IsInsideGroup);
            }
            catch(Exception e)
            {
                Debug.LogWarning("An exception ocurred when trying to apply recommended styles");
                Debug.LogException(e);
            }
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
            else if (visualElement is Vector3Field vector3Field)
                return new Vector3FieldRecommendedStyle(vector3Field);
            else if (visualElement is Vector2Field vector2Field)
                return new Vector2FieldRecommendedStyle(vector2Field);
            else if (visualElement is ColorField colorField)
                return new ColorFieldRecommendedStyle(colorField);
            else if (visualElement is LayerField layerField)
                return new LayerFieldRecommendedStyle(layerField);
            else if (visualElement is EnumField enumField)
                return new EnumFieldRecommendedStyle(enumField);
            else if (visualElement is TagField tagField)
                return new TagFieldRecommendedStyle(tagField);

            return null;
        }
    }
}