using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Serialization.Json;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class UnitySerializationMainToolbarElementVariableSerializer : IMainToolbarElementVariableSerializer
    {
        private struct SerializableElementGroupDTO
        {
            public Dictionary<string, SerializableElementDTO> SerializableElements;
        }

        private struct SerializableElementDTO
        {
            public string ElementFullTypeName;
            public SerializableVariableDTO[] Variables;
        }

        private struct SerializableVariableDTO
        {
            public ValueHolderType Type;
            public string Key;
            public string SerializedValue;
            public string SerializedValueTypeFullyQualifiedName;
        }

        private IValueSerializer _valuesSerializer;
        private MethodInfo _serializeValueMethod, _deserializeValueMethod;
        
        public UnitySerializationMainToolbarElementVariableSerializer(IValueSerializer valuesSerializer)
        {
            _valuesSerializer = valuesSerializer;

            _serializeValueMethod = _valuesSerializer.GetType().GetMethod("Serialize", BindingFlags.Instance | BindingFlags.Public);
            _deserializeValueMethod = _valuesSerializer.GetType().GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.Public);
        }

        public string Serialize(SerializableElementGroup serializableElementGroup)
        {
            var dto = ToDTO(serializableElementGroup);

            return JsonSerialization.ToJson(dto, new JsonSerializationParameters() { Minified = true });
        }

        public SerializableElementGroup Deserialize(string serializedElementGroup)
        {
            var dto = JsonSerialization.FromJson<SerializableElementGroupDTO>(serializedElementGroup);

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
                    SerializedValue = SerializeValue(v.ValueType, v.Value),
                    SerializedValueTypeFullyQualifiedName = v.ValueType.AssemblyQualifiedName
                }).ToArray()
            };
        }

        private SerializableElement FromDTO(SerializableElementDTO dto)
        {
            return new SerializableElement()
            {
                ElementFullTypeName = dto.ElementFullTypeName,
                Variables = dto.Variables.Select(v =>
                {
                    try
                    {
                        var valueType = Type.GetType(v.SerializedValueTypeFullyQualifiedName);

                        return new SerializableVariable()
                        {
                            Key = v.Key,
                            Type = v.Type,
                            ValueType = valueType,
                            Value = DeserializeValue(valueType, v.SerializedValue)
                        };
                    }
                    catch
                    {
                        return default;
                    }
                })
                .Where(v => !string.IsNullOrEmpty(v.Key))
                .ToArray()
            };
        }

        private string SerializeValue(Type type, object value)
        {
            var genericMethod = _serializeValueMethod.MakeGenericMethod(type);

            return (string)genericMethod.Invoke(_valuesSerializer, new object[] { value });
        }

        private object DeserializeValue(Type type, string serializedValue)
        {
            var genericMethod = _deserializeValueMethod.MakeGenericMethod(type);

            return genericMethod.Invoke(_valuesSerializer, new object[] { serializedValue });
        }
    }
}
