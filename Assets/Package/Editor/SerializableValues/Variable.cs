using System;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal abstract class Variable
    {
        private string _lastValueSerialized;
        private string LastValueSerialized
        {
            get
            {
                if (_lastValueSerialized == null)
                    _lastValueSerialized = Serialize(Get());

                return _lastValueSerialized;
            }

            set => _lastValueSerialized = value;
        }
        private IValueSerializer _valueSerializer;
        private MethodInfo _serializeValueMethod, _deserializeValueMethod;

        public MainToolbarElement Element { get; }
        public Type ValueType { get; }
        public SerializeAttribute Attribute { get; }

        public Variable(MainToolbarElement element, Type valueType, object initialValue, IValueSerializer valueSerializer, SerializeAttribute attribute)
        {
            Element = element;
            ValueType = valueType;
            _valueSerializer = valueSerializer;
            Attribute = attribute;
            _serializeValueMethod = _valueSerializer.GetType().GetMethod("Serialize", BindingFlags.Instance | BindingFlags.Public);
            _deserializeValueMethod = _valueSerializer.GetType().GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.Public);
        }

        public abstract object Get();

        public abstract void Set(object value);

        public bool DidChange()
        {
            var currentValue = Get();

            var currentValueSerialized = Serialize(currentValue);

            return currentValueSerialized != LastValueSerialized;
        }

        private string Serialize(object value)
        {
            var genericMethod = _serializeValueMethod.MakeGenericMethod(ValueType);
            return (string)genericMethod.Invoke(_valueSerializer, new object[] { Get() });
        }

        private object Deserialize(string serialized)
        {
            var genericMethod = _deserializeValueMethod.MakeGenericMethod(ValueType);
            return genericMethod.Invoke(_valueSerializer, new object[] { serialized });
        }

        public void UpdateValue()
        {
            LastValueSerialized = Serialize(Get());
        }
    }
}
