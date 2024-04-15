using System;
using UnityEditor;
using UnityEngine;

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

        public static void DeleteEditorPrefs()
        {
            ServicesAndRepositories.MainToolbarElementOverridesRepository.Clear();
            JsonEditorPrefs.DeleteAll();
            MainToolbarAutomaticExtender.Refresh();
        }

        public static void DeleteEditorPrefsIfUserAccepts()
        {
            ShowDialog(
                "Delete Package Related Editor Prefs",
                $"You are about to delete all Editor Prefs related to {ToolInfo.FRIENDLY_TOOL_NAME}.\nAre you sure you want to continue?",
                "Delete",
                "Cancel",
                DeleteEditorPrefs
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