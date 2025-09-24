using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// a request class for box requests. This is the parent class for most of the request classes
    /// </summary>
    public class BoxRequestEntity
    {
        /// <summary>
        /// The Entity's Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of the item 
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxType? Type { get; set; }
    }
}

/// <summary>
/// The types available for the request
/// </summary>
public enum BoxType
{
    file,
    discussion,
    comment,
    folder,
    retention_policy,
    enterprise,
    user,
    group,
    web_link,
    file_version,
    metadata_template,
    terms_of_service
}
