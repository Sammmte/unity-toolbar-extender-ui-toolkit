using System.Linq;
using UnityEditor.Graphs;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class GroupElement : VisualElement
    {
        private EditorToolbarDropdown _dropdown;
        private VisualElement[] _groupedElements;
        public VisualElement[] GroupedElements => _groupedElements.ToArray();

        public GroupElement(string name)
        {
            this.name = name;
            _dropdown = new EditorToolbarDropdown(name, ShowOrHideDropdown);

            Add(_dropdown);
        }

        public void Initialize(VisualElement[] groupedElements)
        {
            _groupedElements = groupedElements;
        }

        private void ShowOrHideDropdown()
        {
            GroupDropdownWindowPopupManager.Show(_dropdown.worldBound, _groupedElements);
        }
    }
}