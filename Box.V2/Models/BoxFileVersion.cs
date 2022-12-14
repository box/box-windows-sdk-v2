using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents a version of a file on Box
    /// </summary>
    public class BoxFileVersion : BoxEntity
    {
        public const string FieldSha1 = "sha1";
        public const string FieldName = "name";
        public const string FieldSize = "size";
        public const string FieldUploaderDisplayName = "uploader_display_name";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldModifiedBy = "modified_by";
        public const string FieldTrashedAt = "trashed_at";
        public const string FieldTrashedBy = "trashed_by";
        public const string FieldPurgedAt = "purged_at";
        public const string FieldRestoredAt = "restored_at";
        public const string FieldRestoredBy = "restored_by";
        public const string FieldVersionNumber = "version_number";

        /// <summary>
        /// The sha1 hash of this file
        /// </summary>
        [JsonProperty(PropertyName = FieldSha1)]
        public virtual string Sha1 { get; private set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        /// <summary>
        /// The folder size in bytes
        /// </summary>
        [JsonProperty(PropertyName = FieldSize)]
        public virtual long? Size { get; private set; }

        /// <summary>
        /// The user's name at the time of upload
        /// </summary>
        [JsonProperty(PropertyName = FieldUploaderDisplayName)]
        public virtual string UploaderDisplayName { get; private set; }

        /// <summary>
        /// The time the item was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time the item or its contents were last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }

        /// <summary>
        /// The user who last modified this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedBy)]
        public virtual BoxUser ModifiedBy { get; private set; }

        /// <summary>
        /// The time the item or its contents were trashed at
        /// </summary>
        [JsonProperty(PropertyName = FieldTrashedAt)]
        public virtual DateTimeOffset? TrashedAt { get; private set; }

        /// <summary>
        /// The user who trashed the contents of this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldTrashedBy)]
        public virtual BoxUser TrashedBy { get; private set; }

        /// <summary>
        /// The time the item or its contents were purged at
        /// </summary>
        [JsonProperty(PropertyName = FieldPurgedAt)]
        public virtual DateTimeOffset? PurgedAt { get; private set; }

        /// <summary>
        /// The time the item or its contents were restored at
        /// </summary>
        [JsonProperty(PropertyName = FieldRestoredAt)]
        public virtual DateTimeOffset? RestoredAt { get; private set; }

        /// <summary>
        /// The user who restored the contents of this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldRestoredBy)]
        public virtual BoxUser RestoredBy { get; private set; }

        /// <summary>
        /// The version number of the file version
        /// </summary>
        [JsonProperty(PropertyName = FieldVersionNumber)]
        public virtual string VersionNumber { get; private set; }
    }
}
