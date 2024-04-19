﻿using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct GroupDefinition
    {
        public string GroupId { get; }
        public string GroupName { get; }
        public ToolbarAlign Alignment { get; }
        public int Order { get; }
        public string[] ToolbarElementsIds { get; }

        public GroupDefinition(string groupId, string groupName, ToolbarAlign alignment, int order, string[] toolbarElementsIds)
        {
            GroupId = groupId;
            GroupName = string.IsNullOrEmpty(groupName) ? groupId : groupName;
            Alignment = alignment;
            Order = order;
            ToolbarElementsIds = toolbarElementsIds;
        }

        public bool AreEquals(GroupDefinition other)
        {
            return GroupId == other.GroupId &&
                GroupName == other.GroupName &&
                Alignment == other.Alignment &&
                Order == other.Order &&
                AreEquals(ToolbarElementsIds, other.ToolbarElementsIds);
        }

        private bool AreEquals(string[] types, string[] types2)
        {
            if(types.Length != types2.Length) 
                return false;

            return types.All(typeName => types2.Contains(typeName));
        }
    }
}