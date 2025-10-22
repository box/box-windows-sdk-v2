using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldRuleItemV2025R0 : ISerializable {
        /// <summary>
        /// The identifier of the shield rule.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `shield_rule`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldRuleItemV2025R0TypeField>))]
        public StringEnum<ShieldRuleItemV2025R0TypeField>? Type { get; init; }

        /// <summary>
        /// The category of the shield rule.
        /// </summary>
        [JsonPropertyName("rule_category")]
        public string? RuleCategory { get; init; }

        /// <summary>
        /// The name of the shield rule.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The description of the shield rule.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The priority level of the shield rule.
        /// </summary>
        [JsonPropertyName("priority")]
        [JsonConverter(typeof(StringEnumConverter<ShieldRuleItemV2025R0PriorityField>))]
        public StringEnum<ShieldRuleItemV2025R0PriorityField>? Priority { get; init; }

        /// <summary>
        /// The date and time when the shield rule was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The date and time when the shield rule was last modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        public ShieldRuleItemV2025R0() {
            
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