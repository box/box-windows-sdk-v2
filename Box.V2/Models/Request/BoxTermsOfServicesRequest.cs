using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making folder requests
    /// </summary>
    public class BoxTermsOfServicesRequest : BoxItemRequest
    {
        /// <summary>
        /// The status of Terms of Services object. Either in enabled or disabled mode. 
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// The specified type of Terms of Services object. Either set to managed or external.
        /// </summary>
        [JsonProperty(PropertyName = "tos_type")]
        public string TosType { get; set; }

        /// <summary>
        /// Description associated with Terms of Services object. Can only by null if Terms Of Service status is set to disabled.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
