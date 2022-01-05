using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public const string FieldPurgedAt = "purged_at";
        public const string FieldContentCreatedAt = "content_created_at";
        public const string FieldContentModifiedAt = "content_modified_at";
        public const string FieldCanNonOwnersInvite = "can_non_owners_invite";
        public const string FieldIsExternallyOwned = "is_externally_owned";
        public const string FieldAllowedSharedLinkAccessLevels = "allowed_shared_link_access_levels";
        public const string FieldExpiresAt = "expires_at";
        public const string FieldIsCollaborationRestrictedToEnterprise = "is_collaboration_restricted_to_enterprise";
        public const string FieldClassification = "classification";

        /// <summary>
        /// The upload email address for this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderUploadEmail)]
        public virtual BoxEmail FolderUploadEmail { get; private set; }

        /// <summary>
        /// A collection of mini file and folder objects contained in this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldItemCollection)]
        public virtual BoxCollection<BoxItem> ItemCollection { get; private set; }

        /// <summary>
        /// Indicates whether this folder will be synced by the Box sync clients or not. Can be synced, not_synced, or partially_synced
        /// </summary>
        [JsonProperty(PropertyName = FieldSyncState)]
        public virtual string SyncState { get; private set; }

        /// <summary>
        /// Indicates whether this folder is a collaboration folder or not
        /// </summary>
        [JsonProperty(PropertyName = FieldHasCollaborations)]
        public virtual bool? HasCollaborations { get; private set; }

        /// <summary>
        /// The available permissions on this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldPermissions)]
        public virtual BoxFolderPermission Permissions { get; protected set; }

        /// <summary>
        /// The available roles that can be used to invite people to the folder
        /// WARNING: This property is still in development and may change!
        /// </summary>
        [JsonProperty(PropertyName = FieldAllowedInviteeRoles)]
        public virtual IList<string> AllowedInviteeRoles { get; protected set; }

        /// <summary>
        /// Information about the watermark status of this folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldWatermarkInfo)]
        public virtual BoxWatermarkInfo WatermarkInfo { get; protected set; }

        /// <summary>
        /// Metadata on this file.
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public virtual dynamic Metadata { get; protected set; }

        /// <summary>
        /// Purged at timestamp for folder
        /// </summary>
        [JsonProperty(PropertyName = FieldPurgedAt)]
        public virtual DateTimeOffset? PurgedAt { get; set; }

        /// <summary>
        /// Content created at timestamp for folder
        /// </summary>
        [JsonProperty(PropertyName = FieldContentCreatedAt)]
        public virtual DateTimeOffset? ContentCreatedAt { get; set; }

        /// <summary>
        /// Content modified at timestamp for folder
        /// </summary>
        [JsonProperty(PropertyName = FieldContentModifiedAt)]
        public virtual DateTimeOffset? ContentModifiedAt { get; set; }

        /// <summary>
        /// Can owners invite field for folder
        /// </summary>
        [JsonProperty(PropertyName = FieldCanNonOwnersInvite)]
        public virtual bool? CanNonOwnersInvite { get; set; }

        /// <summary>
        /// Allowed shared link access levels for folder
        /// </summary>
        [JsonProperty(PropertyName = FieldAllowedSharedLinkAccessLevels)]
        public virtual IList<string> AllowedSharedLinkAccessLevels { get; set; }

        /// <summary>
        /// Is folder externally owned
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExternallyOwned)]
        public virtual bool? IsExternallyOwned { get; set; }

        /// <summary>
        /// The date when the folder will be automatically deleted due to item expiration settings.
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public virtual DateTimeOffset? ExpiresAt { get; protected set; }

        /// <summary>
        /// The date when the folder will be automatically deleted due to item expiration settings.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsCollaborationRestrictedToEnterprise)]
        public virtual bool? IsCollaborationRestrictedToEnterprise { get; protected set; }

        /// <summary>
        /// Represents the classification information for a File on Box.
        /// </summary>
        [JsonProperty(PropertyName = FieldClassification)]
        public virtual BoxClassification Classification { get; private set; }
    }
}
