using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct GroupDefinition
    {
        public string GroupId { get; }
        public ToolbarAlign Alignment { get; }
        public int Order { get; }
        public string[] ToolbarElementsIds { get; }

        public GroupDefinition(string groupId, ToolbarAlign alignment, int order, string[] toolbarElementsIds)
        {
            GroupId = groupId;
            Alignment = alignment;
            Order = order;
            ToolbarElementsIds = toolbarElementsIds;
        }

        public bool AreEquals(GroupDefinition other)
        {
            return other.GroupId == other.GroupId &&
                other.Alignment == other.Alignment &&
                other.Order == other.Order &&
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