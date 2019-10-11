using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents box upload session
    /// </summary>
    public class BoxFileUploadSession : BoxEntity
    {
        public const string FieldSessionExpiresAt = "session_expires_at";
        public const string FieldPartSize = "part_size";
        public const string FieldSessionEndpoints = "session_endpoints";
        public const string FieldTotalParts = "total_parts";
        public const string FieldNumPartsProcessed = "num_parts_processed";

        /// <summary>
        /// Session expiration time in RFC 3339.
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionExpiresAt)]
        public virtual string SessionExpiresAt { get; private set; }

        /// <summary>
        /// The part sizein bytesthat must be used for all parts of this session. Only the last part is allowed to be of a smaller size.
        /// </summary>
        [JsonProperty(PropertyName = FieldPartSize)]
        public virtual string PartSize { get; private set; }

        /// <summary>
        /// URLs for all other possible calls to this session.
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionEndpoints)]
        public virtual BoxSessionEndpoint SessionEndpoints { get; private set; }

        /// <summary>
        /// Total number of parts.
        /// </summary>
        [JsonProperty(PropertyName = FieldTotalParts)]
        public virtual int TotalParts { get; private set; }

        /// <summary>
        /// Number of parts processed.
        /// </summary>
        [JsonProperty(PropertyName = FieldNumPartsProcessed)]
        public virtual int NumPartsProcessed { get; private set; }
    }
}
