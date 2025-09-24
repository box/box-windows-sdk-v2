using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a user invite
    /// </summary>
    public class BoxUserInvite : BoxEntity
    {
        public const string FieldEnterprise = "enterprise";
        public const string FieldActionableBy = "actionable_by";
        public const string FieldInvitedTo = "invited_to";
        public const string FieldInvitedBy = "invited_by";
        public const string FieldStatus = "status";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// Box representation of who can act on this user invitation
        /// </summary>
        [JsonProperty(PropertyName = FieldActionableBy)]
        public virtual BoxUser ActionableBy { get; private set; }

        /// <summary>
        /// Box representation of the enterprise information of a user invitation
        /// </summary>
        [JsonProperty(PropertyName = FieldInvitedTo)]
        public virtual BoxEnterprise InvitedTo { get; private set; }

        /// <summary>
        /// Box representation of the owner of a user invitation
        /// </summary>
        [JsonProperty(PropertyName = FieldInvitedBy)]
        public virtual BoxUser InvitedBy { get; private set; }

        /// <summary>
        /// The status of this invite. Can be accepted, pending, or rejected
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public virtual string Status { get; set; }

        /// <summary>
        /// When this invite was created on Box's servers
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// When this invite was last updated on Box's servers
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }
    }
}
