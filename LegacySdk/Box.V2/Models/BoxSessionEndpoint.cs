using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents box upload session
    /// </summary>
    public class BoxSessionEndpoint
    {
        public const string FieldListParts = "list_parts";
        public const string FieldCommit = "commit";
        public const string FieldLogEvent = "log_event";
        public const string FieldUploadPart = "upload_part";
        public const string FieldAbort = "abort";
        public const string FieldStatus = "status";

        /// <summary>
        /// Endpoint to list parts.
        /// </summary>
        [JsonProperty(PropertyName = FieldListParts)]
        public virtual string ListParts { get; private set; }

        /// <summary>
        /// Endpoint to commit.
        /// </summary>
        [JsonProperty(PropertyName = FieldCommit)]
        public virtual string Commit { get; private set; }

        /// <summary>
        /// Endpoint to log event.
        /// </summary>
        [JsonProperty(PropertyName = FieldLogEvent)]
        public virtual string LogEvent { get; private set; }

        /// <summary>
        /// Endpoint to upload part.
        /// </summary>
        [JsonProperty(PropertyName = FieldUploadPart)]
        public virtual string UploadPart { get; private set; }

        /// <summary>
        /// Endpoint to abort.
        /// </summary>
        [JsonProperty(PropertyName = FieldAbort)]
        public virtual string Abort { get; private set; }

        /// <summary>
        /// Endpoint to get status.
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public virtual string Status { get; private set; }
    }
}
