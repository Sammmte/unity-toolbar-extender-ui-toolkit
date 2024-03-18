using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public sealed class ToolbarSpace
    {
        private Dictionary<string, VisualElement> _groupedElements = new Dictionary<string, VisualElement>();

        public VisualElement VisualElement { get; }

        public ToolbarSpace(FlexDirection flexDirection)
        {
            VisualElement = CreateContainer(flexDirection);
        }

        public void Add(VisualElement element)
        {
            VisualElement.Add(element);
        }

        public void Remove(VisualElement element)
        {
            VisualElement.Remove(element);
        }

        public void AddToGroup(string groupName, VisualElement element)
        {

        }

        private VisualElement CreateContainer(FlexDirection flexDirection)
        {
            var container = new VisualElement()
            {
                style = {
                flexGrow = 1,
                flexDirection = flexDirection,
                alignItems = Align.Center,
                alignContent = Align.Center
                }
            };

            return container;
        }
    }
}