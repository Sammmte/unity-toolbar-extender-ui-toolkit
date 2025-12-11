using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct OverridableElement
    {
        public string Id { get; }
        public VisualElement VisualElement { get; }

        public OverridableElement(string id, VisualElement visualElement)
        {
            Id = id;
            VisualElement = visualElement;
        }
    }
}