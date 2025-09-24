using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making folder requests
    /// </summary>
    public class BoxTermsOfServiceUserStatusesRequest : BoxItemRequest
    {
        /// <summary>
        /// The Terms Of Service Object
        /// </summary>
        [JsonProperty(PropertyName = "tos")]
        public BoxTermsOfService TermsOfService { get; set; }

        /// <summary>
        /// The Box User. Default is current Box User.
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public BoxUser User { get; set; }

        /// <summary>
        /// The acceptance Status of the Box Terms Of Service object
        /// </summary>
        [JsonProperty(PropertyName = "is_accepted")]
        public bool IsAccepted { get; set; }
    }

}
