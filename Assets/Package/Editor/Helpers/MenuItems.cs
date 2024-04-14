using System;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class MenuItems
    {
        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Refresh Toolbar Extender", priority = 1)]
        public static void Refresh()
        {
            MainToolbarAutomaticExtender.Refresh();
        }

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Windows/Main Toolbar Control Panel", priority = 12)]
        public static void OpenControlPanel()
        {
            MainToolbarControlPanelWindow.OpenWindow();
        }

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Delete Actions/Reset Overrides", priority = 23)]
        public static void ResetOverrides()
        {
            GlobalActions.ResetOverridesIfUserAccepts();
        }

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Delete Actions/Delete Package Related EditorPrefs", priority = 23)]
        public static void DeletePackageRelatedEditorPrefs()
        {
            GlobalActions.ShowDialog(
                "Delete Package Related Editor Prefs",
                $"You are about to delete all Editor Prefs related to {ToolInfo.FRIENDLY_TOOL_NAME}.\nAre you sure you want to continue?",
                "Delete",
                "Cancel",
                JsonEditorPrefs.DeleteAll
                );
        }
    }
}