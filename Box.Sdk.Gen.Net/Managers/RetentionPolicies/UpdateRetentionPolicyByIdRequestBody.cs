using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateRetentionPolicyByIdRequestBody : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_ispolicy_nameSet")]
        protected bool _isPolicyNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdescriptionSet")]
        protected bool _isDescriptionSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isretention_typeSet")]
        protected bool _isRetentionTypeSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isstatusSet")]
        protected bool _isStatusSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscan_owner_extend_retentionSet")]
        protected bool _isCanOwnerExtendRetentionSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isare_owners_notifiedSet")]
        protected bool _isAreOwnersNotifiedSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscustom_notification_recipientsSet")]
        protected bool _isCustomNotificationRecipientsSet { get; set; }

        protected string? _policyName { get; set; }

        protected string? _description { get; set; }

        protected string? _retentionType { get; set; }

        protected string? _status { get; set; }

        protected bool? _canOwnerExtendRetention { get; set; }

        protected bool? _areOwnersNotified { get; set; }

        protected IReadOnlyList<UserBase>? _customNotificationRecipients { get; set; }

        /// <summary>
        /// The name for the retention policy.
        /// </summary>
        [JsonPropertyName("policy_name")]
        public string? PolicyName { get => _policyName; init { _policyName = value; _isPolicyNameSet = true; } }

        /// <summary>
        /// The additional text description of the retention policy.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get => _description; init { _description = value; _isDescriptionSet = true; } }

        /// <summary>
        /// The disposition action of the retention policy.
        /// This action can be `permanently_delete`, which
        /// will cause the content retained by the policy
        /// to be permanently deleted, or `remove_retention`,
        /// which will lift the retention policy from the content,
        /// allowing it to be deleted by users,
        /// once the retention policy has expired.
        /// You can use `null` if you don't want to change `disposition_action`.
        /// </summary>
        [JsonPropertyName("disposition_action")]
        public string? DispositionAction { get; init; }

        /// <summary>
        /// Specifies the retention type:
        /// 
        /// * `modifiable`: You can modify the retention policy. For example,
        /// you can add or remove folders, shorten or lengthen
        /// the policy duration, or delete the assignment.
        /// Use this type if your retention policy
        /// is not related to any regulatory purposes.
        /// * `non-modifiable`: You can modify the retention policy
        /// only in a limited way: add a folder, lengthen the duration,
        /// retire the policy, change the disposition action
        /// or notification settings. You cannot perform other actions,
        /// such as deleting the assignment or shortening the
        /// policy duration. Use this type to ensure
        /// compliance with regulatory retention policies.
        /// 
        /// When updating a retention policy, you can use
        /// `non-modifiable` type only. You can convert a
        /// `modifiable` policy to `non-modifiable`, but
        /// not the other way around.
        /// </summary>
        [JsonPropertyName("retention_type")]
        public string? RetentionType { get => _retentionType; init { _retentionType = value; _isRetentionTypeSet = true; } }

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
        /// Used to retire a retention policy.
        /// 
        /// If not retiring a policy, do not include this parameter
        /// or set it to `null`.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get => _status; init { _status = value; _isStatusSet = true; } }

        /// <summary>
        /// Determines if the owner of items under the policy
        /// can extend the retention when the original retention
        /// duration is about to end.
        /// </summary>
        [JsonPropertyName("can_owner_extend_retention")]
        public bool? CanOwnerExtendRetention { get => _canOwnerExtendRetention; init { _canOwnerExtendRetention = value; _isCanOwnerExtendRetentionSet = true; } }

        /// <summary>
        /// Determines if owners and co-owners of items
        /// under the policy are notified when
        /// the retention duration is about to end.
        /// </summary>
        [JsonPropertyName("are_owners_notified")]
        public bool? AreOwnersNotified { get => _areOwnersNotified; init { _areOwnersNotified = value; _isAreOwnersNotifiedSet = true; } }

        /// <summary>
        /// A list of users notified when the retention duration is about to end.
        /// </summary>
        [JsonPropertyName("custom_notification_recipients")]
        public IReadOnlyList<UserBase>? CustomNotificationRecipients { get => _customNotificationRecipients; init { _customNotificationRecipients = value; _isCustomNotificationRecipientsSet = true; } }

        public UpdateRetentionPolicyByIdRequestBody() {
            
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