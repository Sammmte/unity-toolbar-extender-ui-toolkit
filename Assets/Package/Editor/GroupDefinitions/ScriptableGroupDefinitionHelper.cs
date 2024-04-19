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

        public static IEnumerable<string> GetUnusedMainToolbarElementIds(IEnumerable<string> allIds)
        {
            return allIds.Except(GetUsedMainToolbarElementIds());
        }

        private static IEnumerable<string> GetUsedMainToolbarElementIds()
        {
            return _projectGroupDefinitions
                .SelectMany(groupDefinition => groupDefinition.ToolbarElementsIds);
        }

        public static IEnumerable<string> GetGroupIds()
        {
            return _projectGroupDefinitions.Select(g => g.GroupId);
        }

        public static IEnumerable<string> GetEligibleGroupChildsFor(string groupId)
        {
            var allUsedIds = _projectGroupDefinitions.SelectMany(g => g.ToolbarElementsIds);

            return _projectGroupDefinitions.Select(g => g.GroupId)
                .Where(id => !allUsedIds.Contains(id))
                .Where(id => id != groupId);
        }

        private static void OnProjectChange()
        {
            Refresh();
        }
    }
}
