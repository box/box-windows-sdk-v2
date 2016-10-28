using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making email requests
    /// </summary>
    public class BoxEmailRequest
    {
        /// <summary>
        /// The level of access required for this folder upload email. Can be open or collaborators, or null which will be the default value
        /// </summary>
        [JsonProperty(PropertyName = "access")]
        public string Access { get; set; }
    }
}
