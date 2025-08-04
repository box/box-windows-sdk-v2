using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class RetentionPolicyMini : RetentionPolicyBase, ISerializable {
        /// <summary>
        /// The name given to the retention policy.
        /// </summary>
        [JsonPropertyName("policy_name")]
        public string? PolicyName { get; init; }

        /// <summary>
        /// The length of the retention policy. This value
        /// specifies the duration in days that the retention
        /// policy will be active for after being assigned to
        /// content.  If the policy has a `policy_type` of
        /// `indefinite`, the `retention_length` will also be
        /// `indefinite`.
        /// </summary>
        [JsonPropertyName("retention_length")]
        public string? RetentionLength { get; init; }

        /// <summary>
        /// The disposition action of the retention policy.
        /// This action can be `permanently_delete`, which
        /// will cause the content retained by the policy
        /// to be permanently deleted, or `remove_retention`,
        /// which will lift the retention policy from the content,
        /// allowing it to be deleted by users,
        /// once the retention policy has expired.
        /// </summary>
        [JsonPropertyName("disposition_action")]
        [JsonConverter(typeof(StringEnumConverter<RetentionPolicyMiniDispositionActionField>))]
        public StringEnum<RetentionPolicyMiniDispositionActionField>? DispositionAction { get; init; }

        public RetentionPolicyMini(string id, RetentionPolicyBaseTypeField type = RetentionPolicyBaseTypeField.RetentionPolicy) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal RetentionPolicyMini(string id, StringEnum<RetentionPolicyBaseTypeField> type) : base(id, type ?? new StringEnum<RetentionPolicyBaseTypeField>(RetentionPolicyBaseTypeField.RetentionPolicy)) {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}