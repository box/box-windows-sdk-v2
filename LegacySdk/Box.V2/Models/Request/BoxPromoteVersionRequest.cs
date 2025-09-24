using Box.V2.Config;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making promote version requests.
    /// </summary>
    public class BoxPromoteVersionRequest
    {
        /// <summary>
        /// The ID of the file_version that you want to make current.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Must be ‘file_version’ for this request.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return Constants.TypeFileVersion;
            }
        }
    }
}
