using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MainToolbarElementAttribute : Attribute
    {
        public ToolbarAlign AlignWhenSingle { get; }
        public int Order { get; }

        public MainToolbarElementAttribute(ToolbarAlign alignWhenSingle = ToolbarAlign.Left, 
            int order = 0)
        {
            AlignWhenSingle = alignWhenSingle;
            Order = order;
        }
    }
}