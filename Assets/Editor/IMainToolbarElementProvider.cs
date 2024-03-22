using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public interface IMainToolbarElementProvider
    {
        public VisualElement GetElement(bool isGrouped);
    }
}