using System;
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
            ShowDialog(
                "Reset Overrides",
                "You are about to reset all toolbar elements overrides.\nAre you sure you want to continue?",
                "Reset",
                "Cancel",
                () =>
                {
                    ServicesAndRepositories.MainToolbarElementOverridesRepository.Clear();
                    MainToolbarAutomaticExtender.Refresh();
                }
                );
        }

        [MenuItem(ToolInfo.EDITOR_MENU_BASE + "/Delete Package Related EditorPrefs")]
        public static void DeletePackageRelatedEditorPrefs()
        {
            ShowDialog(
                "Delete Package Related Editor Prefs",
                $"You are about to delete all Editor Prefs related to {ToolInfo.FRIENDLY_TOOL_NAME}.\nAre you sure you want to continue?",
                "Delete",
                "Cancel",
                () => JsonEditorPrefs.DeleteAll()
                );
        }

        private static void ShowDialog(string title, string message, string okMessage, 
            string cancelMessage, Action onOk = null, Action onCancel = null)
        {
            if(EditorUtility.DisplayDialog(title, message, okMessage, cancelMessage))
                onOk?.Invoke();
            else
                onCancel?.Invoke();
        }

    }
}