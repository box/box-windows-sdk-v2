using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents box upload session
    /// </summary>
    public class BoxSessionUploadStatus
    {
        public const string FieldSessionExpiryDate = "session_expires_at";
        public const string FieldPartSize = "part_size";
        public const string FieldTotalParts = "total_parts";
        public const string FieldNumberOfPartsProcessed = "num_parts_processed";
        
        /// <summary>
        /// Endpoint to list parts.
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionExpiryDate)]
        public DateTime SessionExpiryDate { get; private set; }

        /// <summary>
        /// Endpoint to commit.
        /// </summary>
        [JsonProperty(PropertyName = FieldPartSize)]
        public long PartSize { get; private set; }

        /// <summary>
        /// Endpoint to log event.
        /// </summary>
        [JsonProperty(PropertyName = FieldTotalParts)]
        public int TotalParts { get; private set; }

        /// <summary>
        /// Endpoint to upload part.
        /// </summary>
        [JsonProperty(PropertyName = FieldNumberOfPartsProcessed)]
        public int NumberOfPartsProcessed { get; private set; }
    }
}
