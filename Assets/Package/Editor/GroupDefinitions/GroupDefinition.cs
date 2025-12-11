using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct GroupDefinition
    {
        public string GroupId { get; }
        public string GroupName { get; }
        public int Order { get; }
        public string[] ToolbarElementsIds { get; }

        public GroupDefinition(string groupId, string groupName, int order, string[] toolbarElementsIds)
        {
            GroupId = groupId;
            GroupName = string.IsNullOrEmpty(groupName) ? groupId : groupName;
            Order = order;
            ToolbarElementsIds = toolbarElementsIds;
        }

        public bool AreEquals(GroupDefinition other)
        {
            return GroupId == other.GroupId &&
                GroupName == other.GroupName &&
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