using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataCascadePolicyOwnerEnterpriseField : ISerializable {
        /// <summary>
        /// The value will always be `enterprise`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<MetadataCascadePolicyOwnerEnterpriseTypeField>))]
        public StringEnum<MetadataCascadePolicyOwnerEnterpriseTypeField>? Type { get; init; }

        /// <summary>
        /// The ID of the enterprise that owns the policy.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public MetadataCascadePolicyOwnerEnterpriseField() {
            
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