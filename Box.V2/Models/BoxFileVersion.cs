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
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldModifiedBy = "modified_by";

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
    }
}