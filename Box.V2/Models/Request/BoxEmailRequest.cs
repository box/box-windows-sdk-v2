using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making email requests
    /// </summary>
    public class BoxEmailRequest
    {
        [JsonProperty(PropertyName = "access")]
        public string Access { get; set; }
    }
}
