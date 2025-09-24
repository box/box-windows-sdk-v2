using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    public class BoxAssignmentRequest
    {
        /// <summary>
        /// The ID of the user this assignment is for.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The login email address for the user this assignment is for.
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }
    }
}
