namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class JsonEditorPrefs
    {
        public static readonly JsonEditorPrefsRepository ToolDataRepository = 
            new JsonEditorPrefsRepository("unity-toolbar-extender-ui-toolkit-editor-prefs");

        public static readonly JsonEditorPrefsRepository UserDataRepository =
            new JsonEditorPrefsRepository("unity-toolbar-extender-ui-toolkit-editor-prefs-user");
    }
}
