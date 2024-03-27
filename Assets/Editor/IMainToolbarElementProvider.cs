using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public interface IMainToolbarElementProvider
    {
        public VisualElement CreateElement(bool isGrouped);
    }
}