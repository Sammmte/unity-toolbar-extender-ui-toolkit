using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MenuItemsTools
{
    private const string DISTRIBUTED_SAMPLES_DIRECTORY = "Assets/Package/Samples~";
    private const string PROJECT_SAMPLES_DIRECTORY = "Assets/Samples";
    private const string DISTRIBUTED_README_PATH = "Assets/Package/README.md";
    private const string README_RESOURCES_PATH_FROM_PACKAGE = "Readme-Resources~";
    private const string README_RESOURCES_PATH_FROM_ROOT = "Assets/Package/Readme-Resources~";

    [MenuItem("Paps/Unity Toolbar Extender UI Toolkit/Debug EditorPrefs Content", priority = 1)]
    public static void DebugEditorPrefsContent()
    {
        var json = EditorPrefs.GetString("unity-toolbar-extender-ui-toolkit-editor-prefs");

        json = JsonPrettify(json);

        Debug.Log(json);
    }

    public static string JsonPrettify(string json)
    {
        using (var stringReader = new StringReader(json))
        using (var stringWriter = new StringWriter())
        {
            var jsonReader = new JsonTextReader(stringReader);
            var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
            jsonWriter.WriteToken(jsonReader);
            return stringWriter.ToString();
        }
    }

    [MenuItem("Paps/Unity Toolbar Extender UI Toolkit/Update Samples And Readme", priority = 1)]
    public static void UpdateSamplesAndReadme()
    {
        DeletePreviousSamples();
        CopyFilesRecursively(PROJECT_SAMPLES_DIRECTORY, DISTRIBUTED_SAMPLES_DIRECTORY);
        SyncPackageReadmeWithRoot();
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

    private static void SyncPackageReadmeWithRoot()
    {
        var markdownText = File.ReadAllText(DISTRIBUTED_README_PATH);

        markdownText = markdownText.Replace(README_RESOURCES_PATH_FROM_PACKAGE,
            README_RESOURCES_PATH_FROM_ROOT);

        var rootPath = Application.dataPath.Replace("/Assets", "");
        var rootReadmePath = Path.Combine(rootPath, "README.md");

        File.WriteAllText(rootReadmePath, markdownText);
    }
}
