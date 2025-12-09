using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElement
    {
        public string Id { get; }
        public VisualElement VisualElement { get; }
        public bool UseRecommendedStyles { get; }

        public MainToolbarElement(string id, VisualElement visualElement, bool useRecommendedStyles)
        {
            Id = id;
            VisualElement = visualElement;
            UseRecommendedStyles = useRecommendedStyles;
        }
    }
}