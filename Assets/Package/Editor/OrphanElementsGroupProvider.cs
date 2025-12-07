using UnityEditor.Toolbars;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class OrphanElementsGroupProvider
    {
        [UnityEditor.Toolbars.MainToolbarElement("UnityToolbarExtenderUIToolkit/Orphan Elements Group",
            defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static UnityEditor.Toolbars.MainToolbarElement CreateOrphanElementsGroup()
        {
            var content = new MainToolbarContent();
            content.text = "Orphan Elements";
            content.tooltip =
                "Any Paps MainToolbarElement that has no Unity MainToolbarElement id or that is not grouped will be shown here";

            var dropdown = new MainToolbarDropdown(content, MainToolbarAutomaticExtender.ShowOrphanElementsGroup);

            return dropdown;
        }
    }
}