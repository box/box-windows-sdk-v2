using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a file
    /// </summary>
    public class BoxFileLock : BoxEntity
    {
        public const string FieldCreatedAt = "created_at";
        public const string FieldCreatedBy = "created_by";
        public const string FieldExpiresAt = "expires_at";
        public const string FieldIsDownloadPrevented = "is_download_prevented";
        public const string FieldFile = "file";

        /// <summary>
        /// The time the lock was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The user who created this lock
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The expiration date of this lock
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public virtual DateTimeOffset? ExpiresAt { get; set; }

        /// <summary>
        /// Is download prevented for this lock?
        /// </summary>
        [JsonProperty(PropertyName = FieldIsDownloadPrevented)]
        public virtual bool IsDownloadPrevented { get; set; }

        /// <summary>
        /// The file the lock applies to; only set when the lock appears as the
        /// source of an event.
        /// </summary>
        [JsonProperty(PropertyName = FieldFile)]
        public virtual BoxFile File { get; private set; }

    }
}
