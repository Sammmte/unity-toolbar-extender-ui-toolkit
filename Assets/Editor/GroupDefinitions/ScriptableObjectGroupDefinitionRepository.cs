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
                .Where(scriptableGroupDefinition => !string.IsNullOrEmpty(scriptableGroupDefinition.GroupName))
                .GroupBy(scriptableGroupDefinition => scriptableGroupDefinition.GroupName)
                .Select(scriptableGroupDefinition => scriptableGroupDefinition.First())
                .Select(scriptableGroupDefinition => new GroupDefinition(
                    scriptableGroupDefinition.GroupName, 
                    scriptableGroupDefinition.Alignment,
                    scriptableGroupDefinition.Order,
                    scriptableGroupDefinition.ToolbarElementsTypes
                    )
                )
                .ToArray();
        }
    }
}