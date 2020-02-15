using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box object to order results returned by a metadata query
    /// </summary>
    public class BoxMetadataQueryOrderBy
    {
        /// <summary>
        /// A string which specifies the key property for a field property to order results by
        /// </summary>
        [JsonProperty(PropertyName = "field_key")]
        public string FieldKey { get; set; }

        /// <summary>
        /// A string that specifies the direction to order the results by
        /// </summary>
        [JsonProperty(PropertyName = "direction")]
        public BoxSortDirection Direction { get; set; }
    }
}
