using UnityEditor;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MiddleGroupDefinition
    {
        public string GroupId;
        public string GroupName;
        public ToolbarAlign Alignment;
        public int Order;
        public List<string> ToolbarElementsIds;

        public MiddleGroupDefinition(string groupId, string groupName, ToolbarAlign alignment, int order, string[] toolbarElementsIds)
        {
            GroupId = groupId;
            GroupName = groupName;
            Alignment = alignment;
            Order = order;
            ToolbarElementsIds = toolbarElementsIds.ToList();
        }
    }

    internal class ScriptableObjectGroupDefinitionRepository : IGroupDefinitionRepository
    {
        public GroupDefinition[] GetAll()
        {
            var paths = AssetDatabase.FindAssets("t:" + nameof(ScriptableGroupDefinition))
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid));

            var scriptableGroupDefinitions = paths
                .Select(path => AssetDatabase.LoadAssetAtPath<ScriptableGroupDefinition>(path))
                .Where(scriptableGroupDefinition => !string.IsNullOrEmpty(scriptableGroupDefinition.GroupId))
                .GroupBy(scriptableGroupDefinition => scriptableGroupDefinition.GroupId)
                .Select(scriptableGroupDefinition => scriptableGroupDefinition.First());

            var middleGroupDefinitions = scriptableGroupDefinitions.Select(s => new MiddleGroupDefinition(
                s.GroupId, s.GroupName, s.Alignment, s.Order, s.ToolbarElementsIds
                ))
                // I needed to do this ToArray, otherwise obtaining an element with
                // FirstOrDefault returned what looked like to be a new reference
                // I mean, a new object, with a different reference to a new list. WTF
                // Ultra weird
                .ToArray();

            foreach(var scriptableGroup in scriptableGroupDefinitions)
            {
                var parent = middleGroupDefinitions.FirstOrDefault(m => m.GroupId == scriptableGroup.ParentGroupId);

                if (parent == null)
                    continue;

                parent.ToolbarElementsIds.Add(scriptableGroup.GroupId);
            }

            var groupDefinitions = middleGroupDefinitions
                .Select(middleGroupDefinition => new GroupDefinition(
                    middleGroupDefinition.GroupId,
                    middleGroupDefinition.GroupName,
                    middleGroupDefinition.Alignment,
                    middleGroupDefinition.Order,
                    FilterIds(middleGroupDefinition)
                    )
                )
                .Where(m => m.ToolbarElementsIds.Length > 0)
                .ToArray();

            return groupDefinitions;
        }

        private string[] FilterIds(MiddleGroupDefinition middleGroupDefinition)
        {
            return middleGroupDefinition.ToolbarElementsIds
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .ToArray();
        }
    }
}