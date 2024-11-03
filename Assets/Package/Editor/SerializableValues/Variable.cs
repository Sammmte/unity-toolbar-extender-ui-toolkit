namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal abstract class Variable
    {
        public readonly MainToolbarElement Element;
        private object _lastValue;

        public Variable(MainToolbarElement element, object initialValue)
        {
            Element = element;
            _lastValue = initialValue;
        }

        public abstract object Get();

        public abstract void Set(object value);

        public bool DidChange()
        {
            var currentValue = Get();

            if (currentValue == null)
                return currentValue != _lastValue;
            else
                return !currentValue.Equals(_lastValue);
        }

        public void UpdateValue()
        {
            _lastValue = Get();
        }
    }
}
