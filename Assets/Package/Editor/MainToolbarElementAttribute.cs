using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MainToolbarElementAttribute : Attribute
    {
        public string Id { get; }
        public ToolbarAlign Alignment { get; }
        public int Order { get; }
        public bool UseRecommendedStyles { get; }

        /// <summary>
        /// Mark a class derived from VisualElement to be found by toolbar extender
        /// </summary>
        /// <param name="id">Id of element. Must be unique. In case of collision, first found is used</param>
        /// <param name="alignment">Left or right to play buttons. Ignored if inside a group</param>
        /// <param name="order">Order in which this element will be displayed in toolbar. Ignored if inside a group</param>
        /// <param name="useRecommendedStyles">True if this element should use recommended styles. Set it to false if you want to style the visual element yourself.</param>
        /// <param name="name"></param>
        public MainToolbarElementAttribute(string id, ToolbarAlign alignment = ToolbarAlign.Left, 
            int order = 0,
            bool useRecommendedStyles = true)
        {
            Id = id;
            Alignment = alignment;
            Order = order;
            UseRecommendedStyles = useRecommendedStyles;
        }
    }
}