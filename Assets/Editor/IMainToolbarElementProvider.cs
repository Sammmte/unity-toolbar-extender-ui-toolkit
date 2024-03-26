using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public interface IMainToolbarElementProvider
    {
        public VisualElement VisualElement { get; }

        public VisualElement CreateElement(bool isGrouped);
    }
}