using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making preflight check requests
    /// </summary>
    public class BoxPreflightCheckRequest : BoxItemRequest
    {
        /// <summary>
        /// The size of the file in bytes. Specify 0 for unknown file-sizes
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }
    }
}
