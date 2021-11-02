using Newtonsoft.Json;


namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making user invite requests
    /// </summary>
    public class BoxUserInviteRequest : BoxRequestEntity
    {

        /// <summary>
        /// Mini representation of the enterprise to invite the user to, including the ID of its enterprise
        /// </summary>
        [JsonProperty(PropertyName = "enterprise")]
        public BoxRequestEntity Enterprise { get; set; }

        /// <summary>
        /// Box representation of who receives this user invitation
        /// </summary>
        [JsonProperty(PropertyName = "actionable_by")]
        public BoxActionableByRequest ActionableBy { get; set; }
    }
}
