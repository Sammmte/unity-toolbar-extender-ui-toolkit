using System.Collections;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class FieldVariable : Variable
    {
        public readonly FieldInfo Field;

        public FieldVariable(MainToolbarElement element, FieldInfo field, IEqualityComparer equalityComparer) 
            : base(element, field.FieldType, field.GetValue(element.VisualElement), equalityComparer)
        {
            Field = field;
        }

        public override object Get()
        {
            return Field.GetValue(Element.VisualElement);
        }

        public override void Set(object value)
        {
            Field.SetValue(Element.VisualElement, value);
        }
    }
}
