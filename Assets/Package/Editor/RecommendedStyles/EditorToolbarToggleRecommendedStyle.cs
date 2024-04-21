using UnityEditor.Toolbars;
using UnityEngine.UIElements;
using System.Reflection;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class EditorToolbarToggleRecommendedStyle : RecommendedStyle
    {
        private readonly EditorToolbarToggle _toolbarToggle;
        private Image _iconImageElement;
        private VisualElement _checkmark;

        public EditorToolbarToggleRecommendedStyle(EditorToolbarToggle toolbarToggle)
        {
            _toolbarToggle = toolbarToggle;
            _iconImageElement = _toolbarToggle.Q<Image>();
            _checkmark = _toolbarToggle.Query(className: "unity-toggle__checkmark");
        }

        protected override void ApplyRootElementStyle()
        {
            _iconImageElement.style.width = Length.Auto();
            _toolbarToggle.style.paddingLeft = 3;
        }

        protected override void ApplyInsideGroupStyle()
        {
            _toolbarToggle.AddToClassList("unity-button");
            _toolbarToggle.RemoveFromClassList("unity-toolbar-toggle");
            _toolbarToggle.RemoveFromClassList("unity-editor-toolbar-toggle");
            _checkmark.style.display = DisplayStyle.None;
        }
    }
}