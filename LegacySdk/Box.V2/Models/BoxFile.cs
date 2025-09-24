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
        public const string FieldDispositionAt = "disposition_at";

        /// <summary>
        /// The sha1 hash of this file
        /// </summary>
        [JsonProperty(PropertyName = FieldSha1)]
        public virtual string Sha1 { get; private set; }

        /// <summary>
        /// The file version information for this file
        /// </summary>
        [JsonProperty(PropertyName = FieldFileVersion)]
        public virtual BoxFileVersion FileVersion { get; private set; }

        /// <summary>
        /// When this file will be permanently deleted
        /// </summary>
        [JsonProperty(PropertyName = FieldPurgedAt)]
        public virtual DateTimeOffset? PurgedAt { get; private set; }

        /// <summary>
        /// When the content of this file was created
        /// For more information about content times <see>http://developers.box.com/content-times/</see>
        /// </summary>
        [JsonProperty(PropertyName = FieldContentCreatedAt)]
        public virtual DateTimeOffset? ContentCreatedAt { get; private set; }

        /// <summary>
        /// When the content of this file was last modified
        /// For more information about content times <see>http://developers.box.com/content-times/</see>
        /// </summary>
        [JsonProperty(PropertyName = FieldContentModifiedAt)]
        public virtual DateTimeOffset? ContentModifiedAt { get; private set; }

        /// <summary>
        /// The version of the file
        /// </summary>
        [JsonProperty(PropertyName = FieldVersionNumber)]
        public virtual string VersionNumber { get; private set; }

        /// <summary>
        /// Indicates the suffix, when available, on the file.
        /// </summary>
        [JsonProperty(PropertyName = FieldExtension)]
        public virtual string Extension { get; private set; }

        /// <summary>
        /// The number of comments on a file
        /// </summary>
        [JsonProperty(PropertyName = FieldCommentCount)]
        public virtual int CommentCount { get; private set; }

        /// <summary>
        /// The available permissions on this file
        /// </summary>
        [JsonProperty(PropertyName = FieldPermissions)]
        public virtual BoxFilePermission Permissions { get; protected set; }

        /// <summary>
        /// The available lock on this file
        /// </summary>
        [JsonProperty(PropertyName = FieldLock)]
        public virtual BoxFileLock Lock { get; protected set; }

        /// <summary>
        /// An expiring URL for an embedded preview session in an iframe.
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiringEmbedLink)]
        public virtual BoxExpiringEmbedLink ExpiringEmbedLink { get; protected set; }

        /// <summary>
        /// Information about the watermark status of this file.
        /// </summary>
        [JsonProperty(PropertyName = FieldWatermarkInfo)]
        public virtual BoxWatermarkInfo WatermarkInfo { get; protected set; }

        /// <summary>
        /// Metadata on this file.
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public virtual dynamic Metadata { get; protected set; }

        /// <summary>
        /// Representation generated for this file. 
        /// </summary>
        [JsonProperty(PropertyName = FieldRepresentations)]
        public virtual BoxRepresentationCollection<BoxRepresentation> Representations { get; protected set; }

        /// <summary>
        /// The date when the file will be automatically deleted due to item expiration settings.
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public virtual DateTimeOffset? ExpiresAt { get; protected set; }

        /// <summary>
        /// The set of allowed roles for collaborators invited to this file.
        /// </summary>
        [JsonProperty(PropertyName = FieldAllowedInviteeRoles)]
        public virtual List<string> AllowedInviteeRoles { get; protected set; }

        /// <summary>
        /// Whether the item has collaborations or not.
        /// </summary>
        [JsonProperty(PropertyName = FieldHasCollaborations)]
        public virtual bool? HasCollaborations { get; protected set; }

        /// <summary>
        /// Whether the item is owned by an entity external to the user's enterprise.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExternallyOwned)]
        public virtual bool? IsExternallyOwned { get; protected set; }

        /// <summary>
        /// The user's name at the time of upload
        /// </summary>
        [JsonProperty(PropertyName = FieldUploaderDisplayName)]
        public virtual string UploaderDisplayName { get; private set; }

        /// <summary>
        /// Represents the classification information for a File on Box.
        /// </summary>
        [JsonProperty(PropertyName = FieldClassification)]
        public virtual BoxClassification Classification { get; private set; }

        /// <summary>
        /// The retention expiration timestamp for the given file.
        /// </summary>
        [JsonProperty(PropertyName = FieldDispositionAt)]
        public virtual DateTimeOffset? DispositionAt { get; private set; }
    }
}
