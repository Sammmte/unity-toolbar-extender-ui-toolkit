using System;
using System.Collections;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class SerializeAttribute : Attribute
    {
        public string SerializationKey { get; }

        public SerializeAttribute(string serializationKey = null)
        {
            SerializationKey = serializationKey;
        }
    }
}
