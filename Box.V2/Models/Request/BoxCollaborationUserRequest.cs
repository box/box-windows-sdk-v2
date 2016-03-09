using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making collaboration user requests
    /// </summary>
    public class BoxCollaborationUserRequest : BoxRequestEntity
    {
        /// <summary>
        /// The ID of this user or group
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Type of collaborator, must be either user or group
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        
        /// <summary>
        /// An email address (does not need to be a Box user)
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

    }
}
