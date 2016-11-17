using Newtonsoft.Json;
using System.Collections.Generic;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a folder
    /// </summary>
    public class BoxFolder : BoxItem
    {
        public const string FieldFolderUploadEmail = "folder_upload_email";
        public const string FieldItemCollection = "item_collection";
        public const string FieldSyncState = "sync_state";
        public const string FieldHasCollaborations = "has_collaborations";
        public const string FieldAllowedInviteeRoles = "allowed_invitee_roles";
        public const string FieldWatermarkInfo = "watermark_info";

        /// <summary>
        /// The upload email address for this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderUploadEmail)]
        public BoxEmail FolderUploadEmail { get; private set; }

        /// <summary>
        /// A collection of mini file and folder objects contained in this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldItemCollection)]
        public BoxCollection<BoxItem> ItemCollection { get; private set; }

        /// <summary>
        /// Indicates whether this folder will be synced by the Box sync clients or not. Can be synced, not_synced, or partially_synced
        /// </summary>
        [JsonProperty(PropertyName = FieldSyncState)]
        public string SyncState { get; private set; }

        /// <summary>
        /// Indicates whether this folder is a collaboration folder or not
        /// </summary>
        [JsonProperty(PropertyName = FieldHasCollaborations)]
        public bool? HasCollaborations { get; private set; }

        /// <summary>
        /// The available permissions on this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldPermissions)]
        public BoxFolderPermission Permissions { get; protected set; }

        /// <summary>
        /// The available roles that can be used to invite people to the folder
        /// WARNING: This property is still in development and may change!
        /// </summary>
        [JsonProperty(PropertyName = FieldAllowedInviteeRoles)]
        public IList<string> AllowedInviteeRoles { get; protected set; }

        /// <summary>
        /// Information about the watermark status of this folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldWatermarkInfo)]
        public BoxWatermarkInfo WatermarkInfo { get; protected set; }
    }
}
