using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct OverridableElement
    {
        public string Id { get; }
        public VisualElement VisualElement { get; }
        public bool IsNative { get; }

        public OverridableElement(string id, VisualElement visualElement, bool isNative)
        {
            Id = id;
            VisualElement = visualElement;
            IsNative = isNative;
        }
    }
}