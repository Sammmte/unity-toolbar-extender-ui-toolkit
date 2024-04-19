using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class RecommendedStyleVisualElement
    {
        public VisualElement VisualElement { get; }
        public bool IsInsideGroup { get; }
        
        public RecommendedStyleVisualElement(VisualElement visualElement, bool isInsideGroup)
        {
            VisualElement = visualElement;
            IsInsideGroup = isInsideGroup;
        }
    }
}