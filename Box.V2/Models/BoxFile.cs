using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a file
    /// </summary>
    public class BoxFile : BoxItem
    {
        public const string FieldSha1 = "sha1";
        public const string FieldPurgedAt = "purged_at";
        public const string FieldContentCreatedAt = "content_created_at";
        public const string FieldContentModifiedAt = "content_modified_at";
        public const string FieldVersionNumber = "version_number";
        public const string FieldExtension = "extension";
        public const string FieldCommentCount = "comment_count";
        public const string FieldLock = "lock";
        public const string FieldExpiringEmbedLink = "expiring_embed_link";
        public const string FieldWatermarkInfo = "watermark_info";
        public const string FieldFileVersion = "file_version";
        public const string FieldRepresentations = "representations";
        public const string FieldExpiresAt = "expires_at";
        public const string FieldAllowedInviteeRoles = "allowed_invitee_roles";
        public const string FieldHasCollaborations = "has_collaborations";
        public const string FieldIsExternallyOwned = "is_externally_owned";
        public const string FieldUploaderDisplayName = "uploader_display_name";
        public const string FieldClassification = "classification";

        /// <summary>
        /// The sha1 hash of this file
        /// </summary>
        [JsonProperty(PropertyName = FieldSha1)]
        public string Sha1 { get; private set; }

        /// <summary>
        /// The file version information for this file
        /// </summary>
        [JsonProperty(PropertyName = FieldFileVersion)]
        public BoxFileVersion FileVersion { get; private set; }

        /// <summary>
        /// When this file will be permanently deleted
        /// </summary>
        [JsonProperty(PropertyName = FieldPurgedAt)]
        public DateTime? PurgedAt { get; private set; }

        /// <summary>
        /// When the content of this file was created
        /// For more information about content times <see>http://developers.box.com/content-times/</see>
        /// </summary>
        [JsonProperty(PropertyName = FieldContentCreatedAt)]
        public DateTime? ContentCreatedAt { get; private set; }

        /// <summary>
        /// When the content of this file was last modified
        /// For more information about content times <see>http://developers.box.com/content-times/</see>
        /// </summary>
        [JsonProperty(PropertyName = FieldContentModifiedAt)]
        public DateTime? ContentModifiedAt { get; private set; }

        /// <summary>
        /// The version of the file
        /// </summary>
        [JsonProperty(PropertyName = FieldVersionNumber)]
        public string VersionNumber { get; private set; }

        /// <summary>
        /// Indicates the suffix, when available, on the file.
        /// </summary>
        [JsonProperty(PropertyName = FieldExtension)]
        public string Extension { get; private set; }

        /// <summary>
        /// The number of comments on a file
        /// </summary>
        [JsonProperty(PropertyName = FieldCommentCount)]
        public int CommentCount { get; private set; }

        /// <summary>
        /// The available permissions on this file
        /// </summary>
        [JsonProperty(PropertyName = FieldPermissions)]
        public BoxFilePermission Permissions { get; protected set; }

        /// <summary>
        /// The available lock on this file
        /// </summary>
        [JsonProperty(PropertyName = FieldLock)]
        public BoxFileLock Lock { get; protected set; }

        /// <summary>
        /// An expiring URL for an embedded preview session in an iframe.
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiringEmbedLink)]
        public BoxExpiringEmbedLink ExpiringEmbedLink { get; protected set; }

        /// <summary>
        /// Information about the watermark status of this file.
        /// </summary>
        [JsonProperty(PropertyName = FieldWatermarkInfo)]
        public BoxWatermarkInfo WatermarkInfo { get; protected set; }

        /// <summary>
        /// Metadata on this file.
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public dynamic Metadata { get; protected set; }

        /// <summary>
        /// Representation generated for this file. 
        /// </summary>
        [JsonProperty(PropertyName = FieldRepresentations)]
        public BoxRepresentationCollection<BoxRepresentation> Representations { get; protected set; }

        /// <summary>
        /// The date when the file will be automatically deleted due to item expiration settings.
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public DateTime? ExpiresAt { get; protected set; }

        /// <summary>
        /// The set of allowed roles for collaborators invited to this file.
        /// </summary>
        [JsonProperty(PropertyName = FieldAllowedInviteeRoles)]
        public List<string> AllowedInviteeRoles { get; protected set; }

        /// <summary>
        /// Whether the item has collaborations or not.
        /// </summary>
        [JsonProperty(PropertyName = FieldHasCollaborations)]
        public bool? HasCollaborations { get; protected set; }

        /// <summary>
        /// Whether the item is owned by an entity external to the user's enterprise.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExternallyOwned)]
        public bool? IsExternallyOwned { get; protected set; }

        /// <summary>
        /// The user's name at the time of upload
        /// </summary>
        [JsonProperty(PropertyName = FieldUploaderDisplayName)]
        public string UploaderDisplayName { get; private set; }

        /// <summary>
        /// Represents the classification information for a File on Box.
        /// </summary>
        [JsonProperty(PropertyName = FieldClassification)]
        public BoxClassification Classification { get; private set; }
    }
}
