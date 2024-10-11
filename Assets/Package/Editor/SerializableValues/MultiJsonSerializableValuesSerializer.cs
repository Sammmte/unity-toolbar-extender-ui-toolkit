﻿using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MultiJsonSerializableValuesSerializer : ISerializableValuesSerializer
    {
        private const string JSON_UTILITY_INVALID_OBJECT_STRING = "{}";

        public Maybe<T> Deserialize<T>(string serializedValue)
        {
            if (IsUnityObjectType<T>())
                return DeserializeUnityObject<T>(serializedValue);
            else
                return InternalDeserialize<T>(serializedValue);
        }

        private bool IsUnityObjectType<T>()
        {
            return typeof(UnityEngine.Object).IsAssignableFrom(typeof(T));
        }

        private Maybe<T> DeserializeUnityObject<T>(string serializedValue)
        {
            var maybe = DeserializeWithJsonNet<SerializedUnityObject>(serializedValue);

            if (maybe.HasValue)
            {
                try
                {
                    var value = (T)(object)maybe.Value.GetValue();
                    return Maybe<T>.Something(value);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("Exception thrown while trying to deserialize Unity Object");
                    Debug.LogException(ex);
                }
            }

            return Maybe<T>.None();
        }

        private Maybe<T> InternalDeserialize<T>(string serializedValue)
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

                if(!IsValidJsonUtilityString(serializedValueWithJsonUtility))
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
            if (IsUnityObjectType<T>())
                return SerializeUnityObject(value);
            else
                return InternalSerialize(value);
        }

        private Maybe<string> SerializeUnityObject<T>(T value)
        {
            var serializedUnityObject = new SerializedUnityObject();

            try
            {
                var unityObject = (UnityEngine.Object)(object)value;
                serializedUnityObject.SetValue(unityObject);
                return SerializeWithJsonNet(serializedUnityObject);
            }
            catch
            {
                return Maybe<string>.None();
            }
        }

        private Maybe<string> InternalSerialize<T>(T value)
        {
            var maybeSerialized = SerializeWithJsonUtility(value);

            if (!maybeSerialized.HasValue)
                return SerializeWithJsonNet(value);

            return maybeSerialized;
        }

        private Maybe<string> SerializeWithJsonUtility<T>(T value)
        {
            try
            {
                var serializedValue = JsonUtility.ToJson(value);

                if (!IsValidJsonUtilityString(serializedValue))
                    return Maybe<string>.None();

                return Maybe<string>.Something(serializedValue);
            }
            catch
            {
                return Maybe<string>.None();
            }
        }

        private Maybe<string> SerializeWithJsonNet<T>(T value)
        {
            try
            {
                var serializedValue = JsonConvert.SerializeObject(value);

                if (!IsValidJsonNetString(serializedValue))
                    return Maybe<string>.None();

                return Maybe<string>.Something(serializedValue);
            }
            catch
            {
                return Maybe<string>.None();
            }
        }

        private bool IsValidJsonUtilityString(string serializedValue)
        {
            return !string.IsNullOrEmpty(serializedValue) && serializedValue != JSON_UTILITY_INVALID_OBJECT_STRING;
        }

        private bool IsValidJsonNetString(string serializedValue)
        {
            return !string.IsNullOrEmpty(serializedValue);
        }
    }
}
