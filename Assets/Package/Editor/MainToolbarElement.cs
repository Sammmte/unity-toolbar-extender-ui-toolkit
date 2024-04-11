using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElement
    {
        public VisualElement VisualElement;
        public ToolbarAlign AlignWhenSingle;
        public int Order;

        public MainToolbarElement(VisualElement visualElement, ToolbarAlign alignWhenSingle, int order)
        {
            VisualElement = visualElement;
            AlignWhenSingle = alignWhenSingle;
            Order = order;
        }
    }
}