using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class RetentionPolicy : RetentionPolicyMini, ISerializable {
        /// <summary>
        /// The additional text description of the retention policy.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The type of the retention policy. A retention
        /// policy type can either be `finite`, where a
        /// specific amount of time to retain the content is known
        /// upfront, or `indefinite`, where the amount of time
        /// to retain the content is still unknown.
        /// </summary>
        [JsonPropertyName("policy_type")]
        [JsonConverter(typeof(StringEnumConverter<RetentionPolicyPolicyTypeField>))]
        public StringEnum<RetentionPolicyPolicyTypeField>? PolicyType { get; init; }

        /// <summary>
        /// Specifies the retention type:
        /// 
        /// * `modifiable`: You can modify the retention policy. For example,
        ///  you can add or remove folders, shorten or lengthen
        ///  the policy duration, or delete the assignment.
        ///  Use this type if your retention policy
        ///  is not related to any regulatory purposes.
        /// 
        /// * `non-modifiable`: You can modify the retention policy
        ///  only in a limited way: add a folder, lengthen the duration,
        ///  retire the policy, change the disposition action
        ///  or notification settings. You cannot perform other actions,
        ///  such as deleting the assignment or shortening the
        ///  policy duration. Use this type to ensure
        ///  compliance with regulatory retention policies.
        /// </summary>
        [JsonPropertyName("retention_type")]
        [JsonConverter(typeof(StringEnumConverter<RetentionPolicyRetentionTypeField>))]
        public StringEnum<RetentionPolicyRetentionTypeField>? RetentionType { get; init; }

        /// <summary>
        /// The status of the retention policy. The status of
        /// a policy will be `active`, unless explicitly retired by an
        /// administrator, in which case the status will be `retired`.
        /// Once a policy has been retired, it cannot become
        /// active again.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<RetentionPolicyStatusField>))]
        public StringEnum<RetentionPolicyStatusField>? Status { get; init; }

        [JsonPropertyName("created_by")]
        public UserMini? CreatedBy { get; init; }

        /// <summary>
        /// When the retention policy object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// When the retention policy object was last modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        /// <summary>
        /// Determines if the owner of items under the policy
        /// can extend the retention when the original
        /// retention duration is about to end.
        /// </summary>
        [JsonPropertyName("can_owner_extend_retention")]
        public bool? CanOwnerExtendRetention { get; init; }

        /// <summary>
        /// Determines if owners and co-owners of items
        /// under the policy are notified when
        /// the retention duration is about to end.
        /// </summary>
        [JsonPropertyName("are_owners_notified")]
        public bool? AreOwnersNotified { get; init; }

        /// <summary>
        /// A list of users notified when the retention policy duration is about to end.
        /// </summary>
        [JsonPropertyName("custom_notification_recipients")]
        public IReadOnlyList<UserMini>? CustomNotificationRecipients { get; init; }

        /// <summary>
        /// Counts the retention policy assignments for each item type.
        /// </summary>
        [JsonPropertyName("assignment_counts")]
        public RetentionPolicyAssignmentCountsField? AssignmentCounts { get; init; }

        public RetentionPolicy(string id, RetentionPolicyBaseTypeField type = RetentionPolicyBaseTypeField.RetentionPolicy) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal RetentionPolicy(string id, StringEnum<RetentionPolicyBaseTypeField> type) : base(id, type ?? new StringEnum<RetentionPolicyBaseTypeField>(RetentionPolicyBaseTypeField.RetentionPolicy)) {
            
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