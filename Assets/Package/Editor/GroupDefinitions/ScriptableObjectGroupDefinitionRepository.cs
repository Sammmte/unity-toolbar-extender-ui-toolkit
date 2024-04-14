using UnityEditor;
using System.Linq;

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
                .Where(scriptableGroupDefinition => scriptableGroupDefinition.ToolbarElementsTypes.Length > 0)
                .GroupBy(scriptableGroupDefinition => scriptableGroupDefinition.GroupId)
                .Select(scriptableGroupDefinition => scriptableGroupDefinition.First())
                .Select(scriptableGroupDefinition => new GroupDefinition(
                    scriptableGroupDefinition.GroupId, 
                    scriptableGroupDefinition.Alignment,
                    scriptableGroupDefinition.Order,
                    scriptableGroupDefinition.ToolbarElementsTypes
                    )
                )
                .ToArray();
        }
    }
}