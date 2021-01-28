using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a retention policy
    /// </summary>
    public class BoxRetentionPolicy : BoxEntity
    {
        public const string FieldPolicyName = "policy_name";
        public const string FieldPolicyType = "policy_type";
        public const string FieldRetentionLength = "retention_length";
        public const string FieldDispositionAction = "disposition_action";
        public const string FieldStatus = "status";
        public const string FieldCreatedBy = "created_by";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldCanOwnerExtendRetention = "can_owner_extend_retention";
        public const string FieldAreOwnersNotified = "are_owners_notified";
        public const string FieldCustomNotificationRecipients = "custom_notification_recipients";

        /// <summary>
        /// The name given to the retention policy
        /// </summary>
        [JsonProperty(PropertyName = FieldPolicyName)]
        public string PolicyName { get; set; }

        /// <summary>
        /// The type of the retention policy. A retention policy type can either be finite, where a specific amount of time to retain the content is known upfront, or indefinite, where the amount of time to retain the content is still unknown.
        /// </summary>
        [JsonProperty(PropertyName = FieldPolicyType)]
        public string PolicyType { get; set; }

        /// <summary>
        /// The length of the retention policy. This length specifies the duration in days that the retention policy will be active for after being assigned to content.
        /// </summary>
        [JsonProperty(PropertyName = FieldRetentionLength)]
        // @TODO(mwiller) 2018-01-29: Change this to the correct type (int)
        public string RetentionLength { get; set; }

        /// <summary>
        /// The disposition action of the retention policy. This action can be permanently_delete, which will cause the content retained by the policy to be permanently deleted, or remove_retention, which will lift the retention policy from the content, allowing it to be deleted by users, once the retention policy time period has passed.
        /// </summary>
        [JsonProperty(PropertyName = FieldDispositionAction)]
        public string DispositionAction { get; set; }

        /// <summary>
        /// The status of a retention policy. The status of a policy will be active, unless explicitly retired by an administrator, in which case the status will be retired. Once a policy has been retired, it cannot become active again.
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public string Status { get; set; }

        /// <summary>
        /// A mini user object representing the user that created the retention policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public BoxUser CreatedBy { get; set; }

        /// <summary>
        /// The time that the retention policy was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The time that the retention policy was last modified.
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Whether owners of items under the policy can extend the retention period.
        /// </summary>
        [JsonProperty(PropertyName = FieldCanOwnerExtendRetention)]
        public bool? CanOwnerExtendRetention { get; set; }

        /// <summary>
        /// Whether owners and co-owners of items under the policy are notified when the retention period is about to end.
        /// </summary>
        [JsonProperty(PropertyName = FieldAreOwnersNotified)]
        public bool? AreOwnersNotified { get; set; }

        /// <summary>
        /// List of additional users who will be notified when the retention period is about to end.
        /// </summary>
        [JsonProperty(PropertyName = FieldCustomNotificationRecipients)]
        public List<BoxUser> CustomNotificationRecipients { get; set; }
    }
}
