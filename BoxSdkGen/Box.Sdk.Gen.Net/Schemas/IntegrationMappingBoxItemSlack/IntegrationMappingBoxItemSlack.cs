using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingBoxItemSlack : ISerializable {
        /// <summary>
        /// Type of the mapped item referenced in `id`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<IntegrationMappingBoxItemSlackTypeField>))]
        public StringEnum<IntegrationMappingBoxItemSlackTypeField> Type { get; }

        /// <summary>
        /// ID of the mapped item (of type referenced in `type`).
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public IntegrationMappingBoxItemSlack(string id, IntegrationMappingBoxItemSlackTypeField type = IntegrationMappingBoxItemSlackTypeField.Folder) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal IntegrationMappingBoxItemSlack(string id, StringEnum<IntegrationMappingBoxItemSlackTypeField> type) {
            Type = IntegrationMappingBoxItemSlackTypeField.Folder;
            Id = id;
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}