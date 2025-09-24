using Newtonsoft.Json;

namespace Box.V2
{
    /// <summary>
    /// Box representation of an Error
    /// </summary>
    public class BoxError
    {
        /// <summary>
        /// The error received. This value will always be present in the event of an error
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public string Name { get; set; }

        /// <summary>
        /// Description of what happened. Provides additional information to the error
        /// </summary>
        [JsonProperty(PropertyName = "error_description")]
        public string Description { get; set; }

        /// <summary>
        /// Status of the response
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// HTTP Status code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Associated message with the error
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Request ID for the request that produced the error.  This is useful for troubleshooting.
        /// </summary>
        [JsonProperty(PropertyName = "request_id")]
        public string RequestId { get; set; }
    }

    /// <summary>
    /// Box representation of a conflict error that includes its context info
    /// </summary>
    /// <typeparam name="T">Type of item that is in conflict</typeparam>
    public class BoxConflictError<T> : BoxError
        where T : class
    {
        /// <summary>
        /// Gets or sets the context information.
        /// </summary>
        /// <value>The context information.</value>
        [JsonProperty(PropertyName = "context_info")]
        public BoxConflictErrorContextInfo<T> ContextInfo { get; set; }
    }

    /// <summary>
    /// Box representation of a preflight check conflict error that includes its context info
    /// </summary>
    /// <typeparam name="T">Type of item that is in conflict</typeparam>
    public class BoxPreflightCheckConflictError<T> : BoxError
        where T : class
    {
        /// <summary>
        /// Gets or sets the context information.
        /// </summary>
        /// <value>The context information.</value>
        [JsonProperty(PropertyName = "context_info")]
        public BoxPreflightCheckConflictErrorContextInfo<T> ContextInfo { get; set; }
    }
}
