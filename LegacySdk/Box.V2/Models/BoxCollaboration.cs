using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a collaboration
    /// </summary>
    public class BoxCollaboration : BoxEntity
    {
        public const string FieldCreatedBy = "created_by";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldExpiresAt = "expires_at";
        public const string FieldStatus = "status";
        public const string FieldAccessibleBy = "accessible_by";
        public const string FieldRole = "role";
        public const string FieldAcknowledgedAt = "acknowledged_at";
        public const string FieldItem = "item";
        public const string FieldCanViewPath = "can_view_path";
        public const string FieldInviteEmail = "invite_email";

        /// <summary>
        /// The user who created this collaboration
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; set; }

        /// <summary>
        /// The time this collaboration was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The time this collaboration was last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; set; }

        /// <summary>
        /// The time this collaboration will expire
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public virtual DateTimeOffset? ExpiresAt { get; set; }

        /// <summary>
        /// The status of this collab. Can be accepted, pending, or rejected
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public virtual string Status { get; set; }

        /// <summary>
        /// The user or group who the collaboration applies to
        /// </summary>
        [JsonProperty(PropertyName = FieldAccessibleBy)]
        public virtual BoxEntity AccessibleBy { get; set; }

        /// <summary>
        /// The level of access this user or group has. Can be editor, viewer, previewer, uploader, previewer uploader, 
        /// viewer uploader, or co-owner
        /// </summary>
        [JsonProperty(PropertyName = FieldRole)]
        public virtual string Role { get; set; }

        /// <summary>
        /// When the status of this collab was changed
        /// </summary>
        [JsonProperty(PropertyName = FieldAcknowledgedAt)]
        public virtual DateTimeOffset? AcknowledgedAt { get; set; }

        /// <summary>
        /// The item this collaboration is related to
        /// </summary>
        [JsonProperty(PropertyName = FieldItem)]
        public virtual BoxItem Item { get; set; }

        /// <summary>
        /// Whether view path collaboration feature is enabled or not. View path collaborations allow the invitee to see the entire ancestral path to the associated folder. 
        /// The user will not gain privileges in any ancestral folder (e.g. see content the user is not collaborated on).
        /// </summary>
        [JsonProperty(PropertyName = FieldCanViewPath)]
        public virtual bool? CanViewPath { get; set; }

        /// <summary>
        /// The email address of the pending collaborator.
        /// </summary>
        [JsonProperty(PropertyName = FieldInviteEmail)]
        public virtual string InviteEmail { get; set; }
    }
}
