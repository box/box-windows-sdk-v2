using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class CollaborationAllowlistEntryEnterpriseField : ISerializable {
        /// <summary>
        /// The unique identifier for this enterprise.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `enterprise`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CollaborationAllowlistEntryEnterpriseTypeField>))]
        public StringEnum<CollaborationAllowlistEntryEnterpriseTypeField>? Type { get; init; }

        /// <summary>
        /// The name of the enterprise.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        public CollaborationAllowlistEntryEnterpriseField() {
            
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