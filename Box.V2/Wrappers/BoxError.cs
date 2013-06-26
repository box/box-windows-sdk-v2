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
    }
}
