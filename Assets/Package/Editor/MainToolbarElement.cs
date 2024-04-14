using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElement
    {
        public string Id { get; }
        public VisualElement VisualElement { get; }
        public ToolbarAlign Alignment { get; }
        public int Order { get; }
        public bool UseRecommendedStyles { get; }

        public MainToolbarElement(string id, VisualElement visualElement, 
            ToolbarAlign alignment, int order, bool useRecommendedStyles)
        {
            Id = id;
            VisualElement = visualElement;
            Alignment = alignment;
            Order = order;
            UseRecommendedStyles = useRecommendedStyles;
        }
    }
}