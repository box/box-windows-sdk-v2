using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingBase : ISerializable {
        /// <summary>
        /// A unique identifier of a folder mapping
        /// (part of a composite key together
        /// with `integration_type`).
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// Mapping type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<IntegrationMappingBaseTypeField>))]
        public StringEnum<IntegrationMappingBaseTypeField> Type { get; }

        public IntegrationMappingBase(string id, IntegrationMappingBaseTypeField type = IntegrationMappingBaseTypeField.IntegrationMapping) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal IntegrationMappingBase(string id, StringEnum<IntegrationMappingBaseTypeField> type) {
            Id = id;
            Type = IntegrationMappingBaseTypeField.IntegrationMapping;
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