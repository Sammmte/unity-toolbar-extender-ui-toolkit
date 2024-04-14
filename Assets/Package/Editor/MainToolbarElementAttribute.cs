using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MainToolbarElementAttribute : Attribute
    {
        public string Id { get; }
        public string Name { get; }
        public ToolbarAlign AlignWhenSingle { get; }
        public int Order { get; }
        public bool UseRecommendedStyles { get; }

        public MainToolbarElementAttribute(string id, ToolbarAlign alignWhenSingle = ToolbarAlign.Left, 
            int order = 0,
            bool useRecommendedStyles = true,
            string name = null)
        {
            Id = id;
            Name = name == null ? Id : name;
            AlignWhenSingle = alignWhenSingle;
            Order = order;
            UseRecommendedStyles = useRecommendedStyles;
        }
    }
}