﻿using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class PropertySerializableVariable
    {
        public readonly MainToolbarElement Element;
        public readonly PropertyInfo Property;
        private object _lastValue;

        public PropertySerializableVariable(MainToolbarElement element, PropertyInfo property)
        {
            Element = element;
            Property = property;
            _lastValue = Get();
        }

        public object Get()
        {
            return Property.GetValue(Element);
        }

        public void Set(object value)
        {
            Property.SetValue(Element, value);
        }

        public bool DidChange()
        {
            var currentValue = Get();

            return !currentValue.Equals(_lastValue);
        }
    }
}
