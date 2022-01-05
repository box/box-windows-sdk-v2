using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents box upload session.
    /// </summary>
    public class BoxSessionUploadStatus
    {
        public const string FieldSessionExpiryDate = "session_expires_at";
        public const string FieldPartSize = "part_size";
        public const string FieldTotalParts = "total_parts";
        public const string FieldNumberOfPartsProcessed = "num_parts_processed";

        /// <summary>
        /// Expiry in DateTimeOffset format for the upload session.
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionExpiryDate)]
        public virtual DateTimeOffset SessionExpiryDate { get; private set; }

        /// <summary>
        /// Size in bytes for the file parts that was returned in Create Session.
        /// </summary>
        [JsonProperty(PropertyName = FieldPartSize)]
        public virtual long PartSize { get; private set; }

        /// <summary>
        /// Total number of parts that are uploaded in the session.
        /// </summary>
        [JsonProperty(PropertyName = FieldTotalParts)]
        public virtual int TotalParts { get; private set; }

        /// <summary>
        /// Total number of parts that have been processed in the backend.
        /// </summary>
        [JsonProperty(PropertyName = FieldNumberOfPartsProcessed)]
        public virtual int NumberOfPartsProcessed { get; private set; }
    }
}
