using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// A request class for a Box item to be included when creating a zip file
    /// </summary>
    public class BoxZipRequestItem
    {
        /// <summary>
        /// The Id of the item 
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of the item
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxZipItemType Type { get; set; }
    }

    /// <summary>
    /// The available types of Box items to be included when creating a zip file
    /// </summary>
    public enum BoxZipItemType
    {
        file,
        folder
    }
}
