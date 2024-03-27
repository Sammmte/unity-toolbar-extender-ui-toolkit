using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class MenuItems
    {
        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Main Toolbar Control Panel")]
        public static void OpenControlPanel()
        {
            MainToolbarControlPanelWindow.OpenWindow();
        }

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Refresh")]
        public static void Refresh()
        {
            MainToolbarAutomaticExtender.Refresh();
        }

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Reset Overrides")]
        public static void ResetOverrides()
        {
            ServicesAndRepositories.MainToolbarElementOverridesRepository.Clear();
            MainToolbarAutomaticExtender.Refresh();
        }
    }
}