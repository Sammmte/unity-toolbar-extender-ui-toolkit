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
            return Field.GetValue(Element.VisualElement);
        }

        public void Set(object value)
        {
            Field.SetValue(Element.VisualElement, value);
        }

        public bool DidChange()
        {
            var currentValue = Get();

            if(currentValue == null)
                return currentValue != _lastValue;
            else
                return!currentValue.Equals(_lastValue);
        }

        public void UpdateValue()
        {
            _lastValue = Get();
        }
    }
}
