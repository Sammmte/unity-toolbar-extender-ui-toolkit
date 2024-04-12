using UnityEditor;
using UnityEngine;

public class MenuItemsTools
{
    [MenuItem("Paps/Unity Toolbar Extender UI Toolkit/Debug EditorPrefs Content", priority = 1)]
    public static void DebugEditorPrefsContent()
    {
        var json = EditorPrefs.GetString("unity-toolbar-extender-ui-toolkit-editor-prefs");

        Debug.Log(json);
    }
}
