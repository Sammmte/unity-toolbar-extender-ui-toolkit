using Newtonsoft.Json;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class JsonMainToolbarElementVariableSerializer : IMainToolbarElementVariableSerializer
    {
        private struct SerializableElementGroupDTO
        {
            [JsonProperty("elements")]
            public Dictionary<string, SerializableElementDTO> SerializableElements;
        }

        private struct SerializableElementDTO
        {
            [JsonProperty("elementType")]
            public string ElementFullTypeName;
            [JsonProperty("variables")]
            public SerializableVariableDTO[] Variables;
        }

        private struct SerializableVariableDTO
        {
            [JsonProperty("type")]
            public ValueHolderType Type;
            [JsonProperty("key")]
            public string Key;
            [JsonProperty("serializedValue")]
            public string SerializedValue;
            [JsonProperty("serializedValueFullTypeName")]
            public string SerializedValueFullTypeName;
        }

        private ISerializableValuesSerializer _valuesSerializer;
        private MethodInfo _serializeValueMethod, _deserializeValueMethod;
        
        public JsonMainToolbarElementVariableSerializer(ISerializableValuesSerializer valuesSerializer)
        {
            _valuesSerializer = valuesSerializer;

            _serializeValueMethod = _valuesSerializer.GetType().GetMethod("Serialize", BindingFlags.Instance | BindingFlags.Public);
            _deserializeValueMethod = _valuesSerializer.GetType().GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.Public);
        }

        public string Serialize(SerializableElementGroup serializableElementGroup)
        {
            var dto = ToDTO(serializableElementGroup);

            return JsonConvert.SerializeObject(dto);
        }

        public SerializableElementGroup Deserialize(string serializedElementGroup)
        {
            var dto = JsonConvert.DeserializeObject<SerializableElementGroupDTO>(serializedElementGroup);

            return FromDTO(dto);
        }

        private SerializableElementGroupDTO ToDTO(SerializableElementGroup group)
        {
            return new SerializableElementGroupDTO()
            {
                SerializableElements = group.SerializableElements.ToDictionary(e => e.Key, e => ToDTO(e.Value))
            };
        }


        private SerializableElementGroup FromDTO(SerializableElementGroupDTO dto)
        {
            return new SerializableElementGroup()
            {
                SerializableElements = dto.SerializableElements == null ? 
                    new Dictionary<string, SerializableElement>() : 
                    dto.SerializableElements.ToDictionary(e => e.Key, e => FromDTO(e.Value))
            };
        }

        private SerializableElementDTO ToDTO(SerializableElement serializableElement)
        {
            return new SerializableElementDTO()
            {
                ElementFullTypeName = serializableElement.ElementFullTypeName,
                Variables = serializableElement.Variables.Select(v => new SerializableVariableDTO()
                {
                    Key = v.Key,
                    Type = v.Type,
                    SerializedValue = SerializeValue(v.Value),
                    SerializedValueFullTypeName = v.Value.GetType().FullName
                }).ToArray()
            };
        }

        private SerializableElement FromDTO(SerializableElementDTO dto)
        {
            return new SerializableElement()
            {
                ElementFullTypeName = dto.ElementFullTypeName,
                Variables = dto.Variables.Select(v => new SerializableVariable()
                {
                    Key = v.Key,
                    Type = v.Type,
                    Value = DeserializeValue(Type.GetType(v.SerializedValueFullTypeName), v.SerializedValue)
                }).ToArray()
            };
        }

        private string SerializeValue(object value)
        {
            var genericMethod = _serializeValueMethod.MakeGenericMethod(value.GetType());

            var maybeSerialized = (Maybe<string>)genericMethod.Invoke(_valuesSerializer, new object[] { value });

            if(maybeSerialized.HasValue)
                return maybeSerialized.Value;

            return null;
        }

        private object DeserializeValue(Type type, string serializedValue)
        {
            var genericMethod = _deserializeValueMethod.MakeGenericMethod(type);

            var maybeDeserialized = genericMethod.Invoke(_valuesSerializer, new object[] { serializedValue });

            var hasValue = (bool)maybeDeserialized.GetType()
                .GetProperty("HasValue", BindingFlags.Public | BindingFlags.Instance)
                .GetValue(maybeDeserialized);

            if (hasValue)
            {
                var value = maybeDeserialized.GetType()
                .GetProperty("Value", BindingFlags.Public | BindingFlags.Instance)
                .GetValue(maybeDeserialized);

                return value;
            }

            return null;
        }
    }
}
