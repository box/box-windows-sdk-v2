using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// Filter for a specific metadata template for files with metadata object associations.
    /// </summary>
    public class BoxMetadataFilterRequest
    {
        /// <summary>
        /// The key name of the template you want to search for. Currently, only searching for one template at a time is supported
        /// </summary>
        [JsonProperty(PropertyName = "templateKey")]
        public string TemplateKey { get; set; }

        /// <summary>
        /// The scope of the template. Currently, only enterprise and global are supported
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Child of mdfilters. Keys and values of the template you want to search within. For numbers and dates, you can include an (inclusive) upper bound parameter lt or (inclusive) lower bound parameter gt or both bounds. An example filter for a “contractExpiration” on or before 08-01-16 would be listed as {"contractExpiration":{"lt":"2016-08-01T00:00-00:00"}}
        /// </summary>
        [JsonProperty(PropertyName = "filters")]
        public object Filters { get; set; }
    }
}
