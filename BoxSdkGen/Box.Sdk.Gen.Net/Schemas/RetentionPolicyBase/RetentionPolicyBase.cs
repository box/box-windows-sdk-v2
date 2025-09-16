using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class RetentionPolicyBase : ISerializable {
        /// <summary>
        /// The unique identifier that represents a retention policy.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `retention_policy`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<RetentionPolicyBaseTypeField>))]
        public StringEnum<RetentionPolicyBaseTypeField> Type { get; }

        public RetentionPolicyBase(string id, RetentionPolicyBaseTypeField type = RetentionPolicyBaseTypeField.RetentionPolicy) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal RetentionPolicyBase(string id, StringEnum<RetentionPolicyBaseTypeField> type) {
            Id = id;
            Type = RetentionPolicyBaseTypeField.RetentionPolicy;
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