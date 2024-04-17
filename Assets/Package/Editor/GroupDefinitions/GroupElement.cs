using System.Linq;
using UnityEditor.Graphs;
using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class GroupElement : VisualElement
    {
        public string GroupName { get; private set; }

        private EditorToolbarDropdown _dropdown;
        private VisualElement[] _groupedElements;
        public VisualElement[] GroupedElements => _groupedElements.ToArray();

        public void Initialize(string groupName, VisualElement[] groupedElements)
        {
            GroupName = groupName;
            name = groupName;
            _groupedElements = groupedElements;
            _dropdown = new EditorToolbarDropdown(groupName, ShowOrHideDropdown);

            Add(_dropdown);
        }

        private void ShowOrHideDropdown()
        {
            GroupDropdownWindowPopupManager.Show(_dropdown.worldBound, _groupedElements);
        }
    }
}