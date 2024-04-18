using System.Linq;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class GroupElement : EditorToolbarDropdown
    {
        private VisualElement[] _groupedElements;
        public VisualElement[] GroupedElements => _groupedElements.ToArray();

        public GroupElement(string name)
        {
            this.name = name;
            text = name;
            clicked += ShowOrHideDropdown;
        }

        public void Initialize(VisualElement[] groupedElements)
        {
            _groupedElements = groupedElements;
        }

        private void ShowOrHideDropdown()
        {
            GroupDropdownWindowPopupManager.Show(worldBound, _groupedElements);
        }
    }
}