using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class FieldSerializableVariable
    {
        public readonly MainToolbarElement Element;
        public readonly FieldInfo Field;
        private object _lastValue;

        public FieldSerializableVariable(MainToolbarElement element, FieldInfo field)
        {
            Element = element;
            Field = field;
            _lastValue = Get();
        }

        public object Get()
        {
            return Field.GetValue(Element);
        }

        public void Set(object value)
        {
            Field.SetValue(Element, value);
        }

        public bool DidChange()
        {
            var currentValue = Get();

            return !currentValue.Equals(_lastValue);
        }
    }
}
