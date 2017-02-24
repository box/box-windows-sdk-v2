using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents box upload session
    /// </summary>
    public class BoxFileUploadSession
    {
        public const string FieldUploadSessionId = "upload_session_id";
        public const string FieldSessionExpiresAt = "session_expires_at";
        public const string FieldPartSize = "part_size";
        public const string FieldSessionEndpoints = "session_endpoints";

        /// <summary>
        /// The upload session id.
        /// </summary>
        [JsonProperty(PropertyName = FieldUploadSessionId)]
        public string UploadSessionId { get; private set; }

        /// <summary>
        /// Session expiration time in RFC 3339.
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionExpiresAt)]
        public string SessionExpiresAt { get; private set; }

        /// <summary>
        /// The part sizein bytesthat must be used for all parts of this session. Only the last part is allowed to be of a smaller size.
        /// </summary>
        [JsonProperty(PropertyName = FieldPartSize)]
        public string PartSize { get; private set; }

        /// <summary>
        /// URLs for all other possible calls to this session.
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionEndpoints)]
        public BoxSessionEndpoint SessionEndpoints { get; private set; }
    }
}
