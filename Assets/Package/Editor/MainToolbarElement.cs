using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElement
    {
        public string Id { get; }
        public VisualElement VisualElement { get; }
        public int Order { get; }
        public bool UseRecommendedStyles { get; }

        public MainToolbarElement(string id, VisualElement visualElement, int order, bool useRecommendedStyles)
        {
            Id = id;
            VisualElement = visualElement;
            Order = order;
            UseRecommendedStyles = useRecommendedStyles;
        }
    }
}