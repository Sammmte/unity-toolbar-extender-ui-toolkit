using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public abstract class SimpleMainToolbarElementProvider : IMainToolbarElementProvider
    {
        public VisualElement VisualElement { get; protected set; }

        public VisualElement CreateElement(bool isGrouped)
        {
            VisualElement = BuildElement(isGrouped);
            OnVisualElementCreated();

            return VisualElement;
        }

        protected abstract VisualElement BuildElement(bool isGrouped);

        protected virtual void OnVisualElementCreated()
        {

        }
    }
}