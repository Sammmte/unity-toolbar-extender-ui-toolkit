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
    }
}