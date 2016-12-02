using Newtonsoft.Json;
using System;

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

        /// <summary>
        /// The user who created this collaboration
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public BoxUser CreatedBy { get; set; }

        /// <summary>
        /// The time this collaboration was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The time this collaboration was last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// The time this collaboration will expire
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// The status of this collab. Can be accepted, pending, or rejected
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public string Status { get; set; }

        /// <summary>
        /// The user or group who the collaboration applies to
        /// </summary>
        [JsonProperty(PropertyName = FieldAccessibleBy)]
        public BoxEntity AccessibleBy { get; set; }

        /// <summary>
        /// The level of access this user or group has. Can be editor, viewer, previewer, uploader, previewer uploader, 
        /// viewer uploader, or co-owner
        /// </summary>
        [JsonProperty(PropertyName = FieldRole)]
        public string Role { get; set; }

        /// <summary>
        /// When the status of this collab was changed
        /// </summary>
        [JsonProperty(PropertyName = FieldAcknowledgedAt)]
        public DateTime? AcknowledgedAt { get; set; }

        /// <summary>
        /// The folder this discussion is related to
        /// </summary>
        [JsonProperty(PropertyName = FieldItem)]
        public BoxFolder Item { get; set; }

        /// <summary>
        /// Whether view path collaboration feature is enabled or not. View path collaborations allow the invitee to see the entire ancestral path to the associated folder. 
        /// The user will not gain privileges in any ancestral folder (e.g. see content the user is not collaborated on).
        /// </summary>
        [JsonProperty(PropertyName = FieldCanViewPath)]
        public bool? CanViewPath { get; set; }
    }
}
