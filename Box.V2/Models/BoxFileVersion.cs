using Newtonsoft.Json;
using System;

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

        /// <summary>
        /// The sha1 hash of this file
        /// </summary>
        [JsonProperty(PropertyName = FieldSha1)]
        public string Sha1 { get; private set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public string Name { get; private set; }

        /// <summary>
        /// The folder size in bytes
        /// </summary>
        [JsonProperty(PropertyName = FieldSize)]
        public long? Size { get; private set; }

        /// <summary>
        /// The user's name at the time of upload
        /// </summary>
        [JsonProperty(PropertyName = FieldUploaderDisplayName)]
        public string UploaderDisplayName { get; private set; }

        /// <summary>
        /// The time the item was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The time the item or its contents were last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; private set; }

        /// <summary>
        /// The user who last modified this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedBy)]
        public BoxUser ModifiedBy { get; private set; }

        /// <summary>
        /// The time the item or its contents were trashed at
        /// </summary>
        [JsonProperty(PropertyName = FieldTrashedAt)]
        public DateTime? TrashedAt { get; private set; }

        /// <summary>
        /// The user who trashed the contents of this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldTrashedBy)]
        public BoxUser TrashedBy { get; private set; }

        /// <summary>
        /// The time the item or its contents were purged at
        /// </summary>
        [JsonProperty(PropertyName = FieldPurgedAt)]
        public DateTime? PurgedAt { get; private set; }

        /// <summary>
        /// The time the item or its contents were restored at
        /// </summary>
        [JsonProperty(PropertyName = FieldRestoredAt)]
        public DateTime? RestoredAt { get; private set; }

        /// <summary>
        /// The user who restored the contents of this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldRestoredBy)]
        public BoxUser RestoredBy { get; private set; }
    }
}
