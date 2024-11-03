using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal struct SerializableVariable
    {
        public ValueHolderType Type;
        public string Key;
        public Type ValueType;
        public object Value;
    }
}
