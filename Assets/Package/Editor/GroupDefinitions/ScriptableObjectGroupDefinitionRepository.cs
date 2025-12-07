using System.Linq;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class ScriptableObjectGroupDefinitionRepository : IGroupDefinitionRepository
    {
        public GroupDefinition[] GetAll()
        {
            var paths = AssetDatabase.FindAssets("t:" + nameof(ScriptableGroupDefinition))
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid));

            return paths
                .Select(path => AssetDatabase.LoadAssetAtPath<ScriptableGroupDefinition>(path))
                .Where(scriptableGroupDefinition => !string.IsNullOrEmpty(scriptableGroupDefinition.GroupId))
                .GroupBy(scriptableGroupDefinition => scriptableGroupDefinition.GroupId)
                .Select(scriptableGroupDefinition => scriptableGroupDefinition.First())
                .Select(scriptableGroupDefinition => new GroupDefinition(
                    scriptableGroupDefinition.GroupId,
                    scriptableGroupDefinition.GroupName,
                    scriptableGroupDefinition.Order,
                    FilterIds(scriptableGroupDefinition)
                    )
                )
                .Where(m => m.ToolbarElementsIds.Length > 0)
                .ToArray();
        }

        private string[] FilterIds(ScriptableGroupDefinition scriptableGroupDefinition)
        {
            return scriptableGroupDefinition.ToolbarElementsIds
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .ToArray();
        }
    }
}