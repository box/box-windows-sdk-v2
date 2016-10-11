using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;

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
        public const string FieldType = "type";
        /// <summary>
        /// Operation type lock or unlock
        /// </summary>
        public enum LockTypes
        {
            /// <summary>
            /// Used for lock file
            /// </summary>
            Lock,
            /// <summary>
            /// Used for unlock file
            /// </summary>
            Unlock
        }
        /// <summary>
        /// The time the lock was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The user who created this lock
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The expiration date of this lock
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Is download prevented for this lock?
        /// </summary>
        [JsonProperty(PropertyName = FieldIsDownloadPrevented)]
        public bool IsDownloadPrevented { get; set; }
        /// <summary>
        /// Can be lock or unlock.
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        [JsonConverter(typeof(StringEnumConverter))]
        public new LockTypes Type { get; set; }
    }
}
