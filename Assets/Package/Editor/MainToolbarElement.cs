using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElement
    {
        public VisualElement VisualElement;
        public ToolbarAlign Alignment;
        public int Order;
        public bool UseRecommendedStyles;

        public MainToolbarElement(VisualElement visualElement, 
            ToolbarAlign alignment, int order, bool useRecommendedStyles)
        {
            VisualElement = visualElement;
            Alignment = alignment;
            Order = order;
            UseRecommendedStyles = useRecommendedStyles;
        }
    }
}