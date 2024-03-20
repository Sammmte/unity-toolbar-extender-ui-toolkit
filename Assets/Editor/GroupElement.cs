using UnityEditor.Toolbars;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class GroupElement : VisualElement
    {
        private EditorToolbarDropdown _dropdown;
        private VisualElement[] _groupedElements;

        public GroupElement(string groupName, VisualElement[] groupedElements)
        {
            _groupedElements = groupedElements;
            _dropdown = new EditorToolbarDropdown(groupName, ShowOrHideDropdown);

            Add(_dropdown);
        }

        private void ShowOrHideDropdown()
        {
            UnityEditor.PopupWindow.Show(_dropdown.worldBound, 
                new GroupElementPopupContent(_dropdown.resolvedStyle.width, _groupedElements));
        }
    }
}