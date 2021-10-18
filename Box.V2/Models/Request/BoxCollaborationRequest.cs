using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for collaboration requests
    /// </summary>
    public class BoxCollaborationRequest : BoxRequestEntity
    {
        /// <summary>
        /// The item to add the collaboration on
        /// The ID and Type are required. The Type can be folder or file.
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public BoxRequestEntity Item { get; set; }

        /// <summary>
        /// The user who this collaboration applies to
        /// </summary>
        [JsonProperty(PropertyName = "accessible_by")]
        public BoxCollaborationUserRequest AccessibleBy { get; set; }

        /// <summary>
        /// The access level of this collaboration. Can be editor, viewer, previewer, uploader, previewer uploader, viewer uploader, co-owner, or owner
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        /// <summary>
        /// Whether this collaboration has been accepted
        /// This can be set to ‘accepted’ or ‘rejected’ by the ‘accessible_by’ user if the status is ‘pending’
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Whether view path collaboration feature is enabled or not. View path collaborations allow the invitee to see the entire ancestral path to the associated folder. 
        /// The user will not gain privileges in any ancestral folder (e.g. see content the user is not collaborated on).
        /// </summary>
        [JsonProperty(PropertyName = "can_view_path")]
        public bool? CanViewPath { get; set; }

        /// <summary>
        /// When the collaboration should expire and be automatically removed.  This value can only be updated if
        /// the collaboration is already set to expire and the user has permission to update the expiration time.
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTimeOffset? ExpiresAt { get; set; }
    }

    public static class BoxCollaborationRoles
    {
        public const string Editor = "editor";
        public const string Viewer = "viewer";
        public const string Previewer = "previewer";
        public const string Uploader = "uploader";
        public const string PreviewerUploader = "previewer uploader";
        public const string ViewerUploader = "viewer uploader";
        public const string CoOwner = "co-owner";
        public const string Owner = "owner";
    }
}
