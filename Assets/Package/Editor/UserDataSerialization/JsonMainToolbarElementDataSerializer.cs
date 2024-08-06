using Newtonsoft.Json;
using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class JsonMainToolbarElementDataSerializer : IMainToolbarElementDataSerializer
    {
        public object Deserialize(string serializedValue, Type type)
        {
            return JsonConvert.DeserializeObject(serializedValue, type);
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
