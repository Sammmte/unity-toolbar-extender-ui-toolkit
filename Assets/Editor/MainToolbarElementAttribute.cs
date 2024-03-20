using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MainToolbarElementAttribute : Attribute
    {
        public ToolbarAlign Align { get; }
        public string Group { get; }
        public int Order { get; }

        public MainToolbarElementAttribute(ToolbarAlign align = ToolbarAlign.Left, string group = "", int order = 0)
        {
            Align = align;
            Group = group;
            Order = order;
        }
    }
}