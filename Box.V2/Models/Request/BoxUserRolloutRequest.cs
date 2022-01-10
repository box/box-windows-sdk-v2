using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for rolling users out of the enterprise.
    /// </summary>
    public class BoxUserRollOutRequest : BoxUserRequest
    {
        /// <summary>
        /// Setting this to null will roll the user out of the enterprise and make them a free user
        /// </summary>
        [JsonProperty(PropertyName = "enterprise", NullValueHandling = NullValueHandling.Include)]
        public new string Enterprise { get; set; }

    }
}
