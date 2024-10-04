using Newtonsoft.Json;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MultiJsonSerializableValuesSerializer : ISerializableValuesSerializer
    {
        private const string EMPTY_OBJECT_STRING = "{}";

        public Maybe<T> Deserialize<T>(string serializedValue)
        {
            var maybeValueWithJsonUtility = DeserializeWithJsonUtility<T>(serializedValue);

            if (!maybeValueWithJsonUtility.HasValue)
                return DeserializeWithJsonNet<T>(serializedValue);

            return maybeValueWithJsonUtility;
        }

        private Maybe<T> DeserializeWithJsonUtility<T>(string serializedValue)
        {
            try
            {
                var value = JsonUtility.FromJson<T>(serializedValue);
                var serializedValueWithJsonUtility = JsonUtility.ToJson(value);

                if(!IsValidString(serializedValueWithJsonUtility))
                    return Maybe<T>.None();

                return Maybe<T>.Something(value);
            }
            catch
            {
                return Maybe<T>.None();
            }
        }

        private Maybe<T> DeserializeWithJsonNet<T>(string serializedValue) 
        {
            try
            {
                var value = JsonConvert.DeserializeObject<T>(serializedValue);

                return Maybe<T>.Something(value);
            }
            catch
            {
                return Maybe<T>.None();
            }
        }

        public Maybe<string> Serialize<T>(T value)
        {
            var serializedValue = JsonUtility.ToJson(value);

            if (!IsValidString(serializedValue))
                serializedValue = JsonConvert.SerializeObject(value);

            if (!IsValidString(serializedValue))
                return Maybe<string>.None();

            return Maybe<string>.Something(serializedValue);
        }

        private bool IsValidString(string serializedValue)
        {
            return !string.IsNullOrEmpty(serializedValue) && serializedValue != EMPTY_OBJECT_STRING;
        }
    }
}
