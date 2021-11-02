using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box item for a metadata query
    /// </summary>
    public class BoxMetadataQueryItem
    {
        /// <summary>
        /// The Box item
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public virtual BoxItem Item { get; private set; }

        /// <summary>
        /// The metadata
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public virtual Dictionary<string, object> Metadata { get; private set; }
    }
}
