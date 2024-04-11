using System;
using System.Linq;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class RefreshToolbarAutomaticExtenderOnSave : AssetModificationProcessor
    {
        private static string[] OnWillSaveAssets(string[] paths)
        {
            var groupDefinitionsAssetsPaths = AssetDatabase.FindAssets("t:" + nameof(ScriptableGroupDefinition))
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .ToArray();

            if (GroupDefinitionAssetIsBeingSaved(paths, groupDefinitionsAssetsPaths))
            {
                EditorApplication.update += RefreshOneTime;
            }

            return paths;
        }

        private static bool GroupDefinitionAssetIsBeingSaved(string[] savingAssetsPaths, string[] groupDefinitionsAssetsPaths)
        {
            return savingAssetsPaths.Any(path => groupDefinitionsAssetsPaths.Contains(path));
        }

        private static void RefreshOneTime()
        {
            EditorApplication.update -= RefreshOneTime;
            Refresh();
        }

        private static void Refresh()
        {
            MainToolbarAutomaticExtender.Refresh();
        }
    }
}