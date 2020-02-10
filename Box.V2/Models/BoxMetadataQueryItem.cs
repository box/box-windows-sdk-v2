using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box item for a metadata query
    /// </summary>
    public class BoxMetadataQueryItem : BoxEntity
    {
        /// <summary>
        /// The Box item
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public BoxItem Item { get; private set; }

        /// <summary>
        /// The metadata
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public Dictionary<string, BoxMetadataTemplate> Metadata { get; private set; }
    }
}
