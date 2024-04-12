using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class RecommendedStyles
    {
        private const string DEFAULT_BUTTON_CLASS = "unity-button";
        private const string BUTTON_CLASS_FOR_TOOLBAR = "unity-toolbar-button";

        public static void Apply(VisualElement visualElement)
        {
            if (visualElement is not EditorToolbarToggle && visualElement is Toggle toggle)
                ApplyFor(toggle, ToggleCallback);
            if (visualElement is not EditorToolbarButton && visualElement is Button button)
                ApplyFor(button, ButtonCallback);
            else if (visualElement is Slider slider)
                ApplyFor(slider, SliderCallback);
            else if(visualElement is DropdownField dropdownField)
                ApplyFor(dropdownField, DropdownFieldCallback);
        }

        private static void ApplyFor(VisualElement visualElement, EventCallback<GeometryChangedEvent> callback)
        {
            visualElement.RegisterCallback(callback);
        }

        private static void ToggleCallback(GeometryChangedEvent eventArgs)
        {
            UnregisterCallback(eventArgs, ToggleCallback);

            var toggle = eventArgs.target as Toggle;

            toggle.labelElement.style.minWidth = 0;
        }

        private static void SliderCallback(GeometryChangedEvent eventArgs)
        {
            UnregisterCallback(eventArgs, SliderCallback);

            var slider = eventArgs.target as Slider;

            slider.labelElement.style.minWidth = 0;
            slider.style.minWidth = 200;
        }

        private static void DropdownFieldCallback(GeometryChangedEvent eventArgs)
        {
            UnregisterCallback(eventArgs, DropdownFieldCallback);

            var dropdownField = eventArgs.target as DropdownField;

            var dropdownElement = dropdownField[1];
            dropdownElement.AddToClassList(BUTTON_CLASS_FOR_TOOLBAR);
        }

        private static void ButtonCallback(GeometryChangedEvent eventArgs)
        {
            UnregisterCallback(eventArgs, ButtonCallback);

            var button = eventArgs.target as Button;

            button.RemoveFromClassList(DEFAULT_BUTTON_CLASS);
            button.AddToClassList(BUTTON_CLASS_FOR_TOOLBAR);
        }

        private static void UnregisterCallback(GeometryChangedEvent eventArgs, EventCallback<GeometryChangedEvent> callback)
        {
            var element = eventArgs.target as VisualElement;

            element.UnregisterCallback(callback);
        }
    }
}