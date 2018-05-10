using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// A request class for making retention policy requests
    /// </summary>
    public class BoxRetentionPolicyRequest
    {
        /// <summary>
        /// Name of retention policy to be created
        /// </summary>
        [JsonProperty(PropertyName = "policy_name")]
        public string PolicyName { get; set; }

        /// <summary>
        /// finite or indefinite
        /// </summary>
        [JsonProperty(PropertyName = "policy_type")]
        public string PolicyType { get; set; }

        /// <summary>
        /// The retention_length is the amount of time, in days, to apply the retention policy to the selected content in days. Do not specify for indefinite policies. Required for finite policies.
        /// </summary>
        [JsonProperty(PropertyName = "retention_length")]
        public int? RetentionLength { get; set; }

        /// <summary>
        /// If creating a finite policy, the disposition action can be permanently_delete or remove_retention. For indefinite policies, disposition action must be remove_retention.
        /// </summary>
        [JsonProperty(PropertyName = "disposition_action")]
        public string DispositionAction { get; set; }

        /// <summary>
        /// Used to retire a retention policy if status is set to retired. If not retiring a policy, do not include or set to null.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Used to determine if the owner of items under the policy can extend the retention when the original period is ending.
        /// </summary>
        [JsonProperty(PropertyName = "can_owner_extend_retention")]
        public bool CanOwnerExtendRetention { get; set; }

        /// <summary>
        /// Used to determine if owners and co-owners of items under the policy are notified when the retention period is ending.
        /// </summary>
        [JsonProperty(PropertyName = "are_owners_notified")]
        public bool AreOwnersNotified { get; set; }

        /// <summary>
        /// List of additional users to notify when the retention period is ending.
        /// </summary>
        [JsonProperty(PropertyName = "custom_notification_recipients")]
        public List<BoxRequestEntity> CustomNotificationRecipients { get; set; }
    }
}
