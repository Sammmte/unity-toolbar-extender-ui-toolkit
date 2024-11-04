using System.Collections;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class PropertyVariable : Variable
    {
        public readonly PropertyInfo Property;

        public PropertyVariable(MainToolbarElement element, PropertyInfo property, IValueSerializer valueSerializer, SerializeAttribute attribute) 
            : base(element, property.PropertyType, property.GetValue(element.VisualElement), valueSerializer, attribute)
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
