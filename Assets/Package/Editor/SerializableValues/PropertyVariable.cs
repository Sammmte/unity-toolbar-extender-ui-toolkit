using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class PropertyVariable : Variable
    {
        public readonly PropertyInfo Property;

        public PropertyVariable(MainToolbarElement element, PropertyInfo property) : base(element, property.GetValue(element.VisualElement))
        {
            Property = property;
        }

        public override object Get()
        {
            return Property.GetValue(Element.VisualElement);
        }

        public override void Set(object value)
        {
            Property.SetValue(Element.VisualElement, value);
        }
    }
}
