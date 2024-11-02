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

            var changed = !currentValue.Equals(_lastValue);

            _lastValue = currentValue;

            return changed;
        }
    }
}
