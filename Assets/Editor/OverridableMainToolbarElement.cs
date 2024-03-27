using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public readonly struct OverridableMainToolbarElement
    {
        public string Id { get; }
        public VisualElement VisualElement { get; }

        public OverridableMainToolbarElement(string id, VisualElement visualElement)
        {
            Id = id;
            VisualElement = visualElement;
        }
    }
}