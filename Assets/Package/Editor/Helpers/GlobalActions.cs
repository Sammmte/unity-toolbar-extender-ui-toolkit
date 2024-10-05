using System;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class GlobalActions
    {
        public static void ResetOverrides()
        {
            ServicesAndRepositories.MainToolbarElementOverridesRepository.Clear();
            MainToolbarAutomaticExtender.Refresh();
        }

        public static void ResetOverridesIfUserAccepts()
        {
            ShowDialog(
                "Reset Overrides",
                "You are about to reset all toolbar elements overrides.\nAre you sure you want to continue?",
                "Reset",
                "Cancel",
                ResetOverrides
                );
        }

        public static void DeleteToolEditorPrefs()
        {
            ServicesAndRepositories.MainToolbarElementOverridesRepository.Clear();
            JsonEditorPrefs.ToolDataRepository.DeleteAll();
            MainToolbarAutomaticExtender.Refresh();
        }

        public static void DeleteToolEditorPrefsIfUserAccepts()
        {
            ShowDialog(
                "Delete Package Related Editor Prefs",
                $"You are about to delete all Editor Prefs {ToolInfo.FRIENDLY_TOOL_NAME} uses for self administration. They will be reconstructed later. Use this if you suspect that some cache data is causing trouble.\nAre you sure you want to continue?",
                "Delete",
                "Cancel",
                DeleteToolEditorPrefs
                );
        }

        public static void DeleteUserEditorPrefs()
        {
            ServicesAndRepositories.MainToolbarElementOverridesRepository.Clear();
            JsonEditorPrefs.UserDataRepository.DeleteAll();
            MainToolbarAutomaticExtender.Refresh();
        }

        public static void DeleteUserEditorPrefsIfUserAccepts()
        {
            ShowDialog(
                "Delete User Related Editor Prefs",
                $"You are about to delete all Editor Prefs {ToolInfo.FRIENDLY_TOOL_NAME} used to store values from your custom elements. All values stored by custom elements will be reset.\nAre you sure you want to continue?",
                "Delete",
                "Cancel",
                DeleteUserEditorPrefs
                );
        }

        public static void ShowDialog(string title, string message, string okMessage,
            string cancelMessage, Action onOk = null, Action onCancel = null)
        {
            if (EditorUtility.DisplayDialog(title, message, okMessage, cancelMessage))
                onOk?.Invoke();
            else
                onCancel?.Invoke();
        }
    }
}