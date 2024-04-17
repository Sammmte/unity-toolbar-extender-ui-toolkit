using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class ScriptableGroupDefinitionHelper
    {
        private static ScriptableGroupDefinition[] _projectGroupDefinitions;

        static ScriptableGroupDefinitionHelper()
        {
            LoadProjectGroupDefinitions();

            EditorApplication.projectChanged += OnProjectChange;
        }

        public static void Refresh()
        {
            LoadProjectGroupDefinitions();
        }

        private static void LoadProjectGroupDefinitions()
        {
            var paths = AssetDatabase.FindAssets("t:" + nameof(ScriptableGroupDefinition))
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid));

            _projectGroupDefinitions = paths
                .Select(path => AssetDatabase.LoadAssetAtPath<ScriptableGroupDefinition>(path))
                .ToArray();
        }

        public static IEnumerable<string> GetUnusedIds(IEnumerable<string> allIds)
        {
            return allIds.Except(GetUsedIds());
        }

        public static bool FirstGroupIsParentOfSecond(string firstGroupId, string secondGroupId)
        {
            var firstGroupDefinition = _projectGroupDefinitions.First(g => g.GroupId == firstGroupId);

            return firstGroupDefinition.ToolbarElementsIds.Contains(secondGroupId);
        }

        private static IEnumerable<string> GetUsedIds()
        {
            return _projectGroupDefinitions
                .SelectMany(groupDefinition => groupDefinition.ToolbarElementsIds);
        }

        private static void OnProjectChange()
        {
            Refresh();
        }
    }
}
