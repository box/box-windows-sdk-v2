using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateRetentionPolicyRequestBody : ISerializable {
        /// <summary>
        /// The name for the retention policy.
        /// </summary>
        [JsonPropertyName("policy_name")]
        public string PolicyName { get; }

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
        [JsonConverter(typeof(StringEnumConverter<CreateRetentionPolicyRequestBodyPolicyTypeField>))]
        public StringEnum<CreateRetentionPolicyRequestBodyPolicyTypeField> PolicyType { get; }

        /// <summary>
        /// The disposition action of the retention policy.
        /// `permanently_delete` deletes the content
        /// retained by the policy permanently.
        /// `remove_retention` lifts retention policy
        /// from the content, allowing it to be deleted
        /// by users once the retention policy has expired.
        /// </summary>
        [JsonPropertyName("disposition_action")]
        [JsonConverter(typeof(StringEnumConverter<CreateRetentionPolicyRequestBodyDispositionActionField>))]
        public StringEnum<CreateRetentionPolicyRequestBodyDispositionActionField> DispositionAction { get; }

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
        /// Specifies the retention type:
        /// 
        /// * `modifiable`: You can modify the retention policy. For example,
        /// you can add or remove folders, shorten or lengthen
        /// the policy duration, or delete the assignment.
        /// Use this type if your retention policy
        /// is not related to any regulatory purposes.
        /// 
        /// * `non_modifiable`: You can modify the retention policy
        /// only in a limited way: add a folder, lengthen the duration,
        /// retire the policy, change the disposition action
        /// or notification settings. You cannot perform other actions,
        /// such as deleting the assignment or shortening the
        /// policy duration. Use this type to ensure
        /// compliance with regulatory retention policies.
        /// </summary>
        [JsonPropertyName("retention_type")]
        [JsonConverter(typeof(StringEnumConverter<CreateRetentionPolicyRequestBodyRetentionTypeField>))]
        public StringEnum<CreateRetentionPolicyRequestBodyRetentionTypeField>? RetentionType { get; init; }

        /// <summary>
        /// Whether the owner of a file will be allowed to
        /// extend the retention.
        /// </summary>
        [JsonPropertyName("can_owner_extend_retention")]
        public bool? CanOwnerExtendRetention { get; init; }

        /// <summary>
        /// Whether owner and co-owners of a file are notified
        /// when the policy nears expiration.
        /// </summary>
        [JsonPropertyName("are_owners_notified")]
        public bool? AreOwnersNotified { get; init; }

        /// <summary>
        /// A list of users notified when
        /// the retention policy duration is about to end.
        /// </summary>
        [JsonPropertyName("custom_notification_recipients")]
        public IReadOnlyList<UserMini>? CustomNotificationRecipients { get; init; }

        public CreateRetentionPolicyRequestBody(string policyName, CreateRetentionPolicyRequestBodyPolicyTypeField policyType, CreateRetentionPolicyRequestBodyDispositionActionField dispositionAction) {
            PolicyName = policyName;
            PolicyType = policyType;
            DispositionAction = dispositionAction;
        }
        
        [JsonConstructorAttribute]
        internal CreateRetentionPolicyRequestBody(string policyName, StringEnum<CreateRetentionPolicyRequestBodyPolicyTypeField> policyType, StringEnum<CreateRetentionPolicyRequestBodyDispositionActionField> dispositionAction) {
            PolicyName = policyName;
            PolicyType = policyType;
            DispositionAction = dispositionAction;
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