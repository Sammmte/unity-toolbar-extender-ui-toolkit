using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MultiJsonSerializableValuesSerializer : ISerializableValuesSerializer
    {
        private struct SerializedUnityObject
        {
            public string AssetGuid;
            public string ComponentTypeFullName;
            public long ComponentFileId;

            public void SetValue(UnityEngine.Object value)
            {
                var assetGuid = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(value)).ToString();

                if (!IsValidAssetGuid(assetGuid))
                    throw new ArgumentException("Object is not a valid asset");

                if (value is Component)
                {
                    ComponentTypeFullName = value.GetType().FullName;
                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(value, out string _, out ComponentFileId);
                }

                AssetGuid = assetGuid;
            }

            private bool IsValidAssetGuid(string assetGuid)
            {
                return !assetGuid.All(character => character == '0');
            }

            public UnityEngine.Object GetValue()
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(new GUID(AssetGuid));
                var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
                
                if (ComponentFileId > 0)
                    obj = GetComponentFromFileId(obj as GameObject);

                return obj;
            }

            private UnityEngine.Object GetComponentFromFileId(GameObject gameObject)
            {
                var allPossibleComponents = gameObject.GetComponents(ComponentTypeNameToType());

                foreach (var component in allPossibleComponents)
                {
                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(component, out string _, out long fileId);

                    if (fileId == ComponentFileId)
                        return component;
                }

                return gameObject;
            }

            private Type ComponentTypeNameToType()
            {
                foreach(var type in TypeCache.GetTypesDerivedFrom<UnityEngine.Object>())
                {
                    if (type.FullName == ComponentTypeFullName)
                        return type;
                }

                return null;
            }
        }

        private const string EMPTY_OBJECT_STRING = "{}";

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

                if (!IsValidString(serializedValue))
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

                if (!IsValidString(serializedValue))
                    return Maybe<string>.None();

                return Maybe<string>.Something(serializedValue);
            }
            catch
            {
                return Maybe<string>.None();
            }
        }

        private bool IsValidString(string serializedValue)
        {
            return !string.IsNullOrEmpty(serializedValue) && serializedValue != EMPTY_OBJECT_STRING;
        }
    }
}
