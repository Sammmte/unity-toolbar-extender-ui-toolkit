using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class FieldVariable : Variable
    {
        public readonly FieldInfo Field;

        public FieldVariable(MainToolbarElement element, FieldInfo field) : base(element, field.GetValue(element.VisualElement))
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
