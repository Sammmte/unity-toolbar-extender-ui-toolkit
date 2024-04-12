using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal readonly struct GroupDefinition
    {
        public string GroupName { get; }
        public ToolbarAlign Alignment { get; }
        public int Order { get; }
        public string[] ToolbarElementsTypes { get; }

        public GroupDefinition(string groupName, ToolbarAlign alignment, int order, string[] toolbarElementsTypes)
        {
            GroupName = groupName;
            Alignment = alignment;
            Order = order;
            ToolbarElementsTypes = toolbarElementsTypes;
        }

        public bool AreEquals(GroupDefinition other)
        {
            return other.GroupName == other.GroupName &&
                other.Alignment == other.Alignment &&
                other.Order == other.Order &&
                AreEquals(ToolbarElementsTypes, other.ToolbarElementsTypes);
        }

        private bool AreEquals(string[] types, string[] types2)
        {
            if(types.Length != types2.Length) 
                return false;

            return types.All(typeName => types2.Contains(typeName));
        }
    }
}