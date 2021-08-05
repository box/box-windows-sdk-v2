using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// A request class for a create a user status for terms of service request.
    /// </summary>
    public class BoxTermsOfServiceUserStatusCreateRequest
    {
        /// <summary>
        /// The Terms Of Service Object.
        /// </summary>
        [JsonProperty(PropertyName = "tos")]
        public BoxRequestEntity TermsOfService { get; set; }

        /// <summary>
        /// The Box User.
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public BoxRequestEntity User { get; set; }

        /// <summary>
        /// The acceptance Status of the Box Terms Of Service object.
        /// </summary>
        [JsonProperty(PropertyName = "is_accepted")]
        public bool IsAccepted { get; set; }
    }
}
