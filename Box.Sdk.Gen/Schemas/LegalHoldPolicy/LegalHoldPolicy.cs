using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class LegalHoldPolicy : LegalHoldPolicyMini, ISerializable {
        /// <summary>
        /// Name of the legal hold policy.
        /// </summary>
        [JsonPropertyName("policy_name")]
        public string? PolicyName { get; init; }

        /// <summary>
        /// Description of the legal hold policy. Optional
        /// property with a 500 character limit.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// Possible values:
        /// * 'active' - the policy is not in a transition state.
        /// * 'applying' - that the policy is in the process of
        ///   being applied.
        /// * 'releasing' - that the process is in the process
        ///   of being released.
        /// * 'released' - the policy is no longer active.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<LegalHoldPolicyStatusField>))]
        public StringEnum<LegalHoldPolicyStatusField>? Status { get; init; }

        /// <summary>
        /// Counts of assignments within this a legal hold policy by item type.
        /// </summary>
        [JsonPropertyName("assignment_counts")]
        public LegalHoldPolicyAssignmentCountsField? AssignmentCounts { get; init; }

        [JsonPropertyName("created_by")]
        public UserMini? CreatedBy { get; init; }

        /// <summary>
        /// When the legal hold policy object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// When the legal hold policy object was modified.
        /// Does not update when assignments are added or removed.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        /// <summary>
        /// When the policy release request was sent. (Because
        /// it can take time for a policy to fully delete, this
        /// isn't quite the same time that the policy is fully deleted).
        /// 
        /// If `null`, the policy was not deleted.
        /// </summary>
        [JsonPropertyName("deleted_at")]
        public System.DateTimeOffset? DeletedAt { get; init; }

        /// <summary>
        /// User-specified, optional date filter applies to
        /// Custodian assignments only.
        /// </summary>
        [JsonPropertyName("filter_started_at")]
        public System.DateTimeOffset? FilterStartedAt { get; init; }

        /// <summary>
        /// User-specified, optional date filter applies to
        /// Custodian assignments only.
        /// </summary>
        [JsonPropertyName("filter_ended_at")]
        public System.DateTimeOffset? FilterEndedAt { get; init; }

        /// <summary>
        /// Optional notes about why the policy was created.
        /// </summary>
        [JsonPropertyName("release_notes")]
        public string? ReleaseNotes { get; init; }

        public LegalHoldPolicy(string id, LegalHoldPolicyMiniTypeField type = LegalHoldPolicyMiniTypeField.LegalHoldPolicy) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal LegalHoldPolicy(string id, StringEnum<LegalHoldPolicyMiniTypeField> type) : base(id, type ?? new StringEnum<LegalHoldPolicyMiniTypeField>(LegalHoldPolicyMiniTypeField.LegalHoldPolicy)) {
            
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