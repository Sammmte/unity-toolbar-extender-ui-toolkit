using Newtonsoft.Json;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;

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
                    SerializedValue = SerializeValue(v.Value)
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
                    Value = DeserializeValue(v.SerializedValue)
                }).ToArray()
            };
        }

        private string SerializeValue(object value)
        {
            return null;
        }

        private object DeserializeValue(string serializedValue)
        {
            return null;
        }
    }
}
