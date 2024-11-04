﻿using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class FieldVariable : Variable
    {
        public readonly FieldInfo Field;

        public FieldVariable(MainToolbarElement element, FieldInfo field, IValueSerializer valueSerializer, SerializeAttribute attribute) 
            : base(element, field.FieldType, field.GetValue(element.VisualElement), valueSerializer, attribute)
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
