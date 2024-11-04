using Unity.Serialization.Json;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class UnitySerializationValueSerializer : IValueSerializer
    {
        public T Deserialize<T>(string serializedValue)
        {
            return JsonSerialization.FromJson<T>(serializedValue);
        }

        public string Serialize<T>(T value)
        {
            return JsonSerialization.ToJson(value, new JsonSerializationParameters() { Minified = true } );
        }
    }
}
