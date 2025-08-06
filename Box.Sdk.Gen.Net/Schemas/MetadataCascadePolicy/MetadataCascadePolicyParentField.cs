using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataCascadePolicyParentField : ISerializable {
        /// <summary>
        /// The value will always be `folder`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<MetadataCascadePolicyParentTypeField>))]
        public StringEnum<MetadataCascadePolicyParentTypeField>? Type { get; init; }

        /// <summary>
        /// The ID of the folder the policy is applied to.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public MetadataCascadePolicyParentField() {
            
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