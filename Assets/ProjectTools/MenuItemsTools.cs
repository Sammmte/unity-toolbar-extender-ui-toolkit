using System.IO;
using UnityEditor;
using UnityEngine;

public class MenuItemsTools
{
    private const string DISTRIBUTED_SAMPLES_DIRECTORY = "Assets/Package/Samples~";
    private const string PROJECT_SAMPLES_DIRECTORY = "Assets/Samples";

    [MenuItem("Paps/Unity Toolbar Extender UI Toolkit/Debug EditorPrefs Content", priority = 1)]
    public static void DebugEditorPrefsContent()
    {
        var json = EditorPrefs.GetString("unity-toolbar-extender-ui-toolkit-editor-prefs");

        Debug.Log(json);
    }

    [MenuItem("Paps/Unity Toolbar Extender UI Toolkit/Update Samples", priority = 1)]
    public static void UpdateSamples()
    {
        DeletePreviousSamples();
        CopyFilesRecursively(PROJECT_SAMPLES_DIRECTORY, DISTRIBUTED_SAMPLES_DIRECTORY);
    }

    private static void DeletePreviousSamples()
    {
        var previousFiles = Directory.GetFiles(DISTRIBUTED_SAMPLES_DIRECTORY);
        var previousDirectories = Directory.GetDirectories(DISTRIBUTED_SAMPLES_DIRECTORY);

        foreach (var file in previousFiles)
            File.Delete(file);

        foreach (var directory in previousDirectories)
            Directory.Delete(directory, true);
    }

    private static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }

        foreach (string filePath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        {
            if (filePath.EndsWith(".meta"))
                continue;

            File.Copy(filePath, filePath.Replace(sourcePath, targetPath), true);
        }
    }
}
