using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class DropdownFieldRecommendedStyle : RecommendedStyle
    {
        private const string BUTTON_CLASS_FOR_TOOLBAR = "unity-toolbar-button";
        private DropdownField _dropdownField;

        public DropdownFieldRecommendedStyle(DropdownField dropdownField)
        {
            _dropdownField = dropdownField;
        }

        protected override void ApplyInsideGroupStyle()
        {
            var dropdownElement = _dropdownField[1];
            dropdownElement.RemoveFromClassList(BUTTON_CLASS_FOR_TOOLBAR);
        }

        protected override void ApplyRootElementStyle()
        {
            var dropdownElement = _dropdownField[1];
            dropdownElement.AddToClassList(BUTTON_CLASS_FOR_TOOLBAR);
        }
    }
}