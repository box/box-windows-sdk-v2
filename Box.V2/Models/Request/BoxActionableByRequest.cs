using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for who can receives this user invitation
    /// </summary>
    public class BoxActionableByRequest
    {
        /// <summary>
        /// The login that will receive a user invite
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }
    }
}
