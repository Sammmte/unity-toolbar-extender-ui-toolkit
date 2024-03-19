using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MainToolbarElementAttribute : Attribute
    {
        public ToolbarAlign Align { get; }
        public string Group { get; }

        public MainToolbarElementAttribute(ToolbarAlign align = ToolbarAlign.Left, string group = "")
        {
            Align = align;
            Group = group;
        }
    }
}