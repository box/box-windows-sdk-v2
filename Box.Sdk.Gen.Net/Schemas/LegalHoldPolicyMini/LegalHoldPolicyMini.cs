using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class LegalHoldPolicyMini : ISerializable {
        /// <summary>
        /// The unique identifier for this legal hold policy.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `legal_hold_policy`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<LegalHoldPolicyMiniTypeField>))]
        public StringEnum<LegalHoldPolicyMiniTypeField> Type { get; }

        public LegalHoldPolicyMini(string id, LegalHoldPolicyMiniTypeField type = LegalHoldPolicyMiniTypeField.LegalHoldPolicy) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal LegalHoldPolicyMini(string id, StringEnum<LegalHoldPolicyMiniTypeField> type) {
            Id = id;
            Type = LegalHoldPolicyMiniTypeField.LegalHoldPolicy;
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