using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal interface IMainToolbarElementDataSerializer
    {
        string Serialize(object value);
        object Deserialize(string serializedValue, Type type);
    }
}
