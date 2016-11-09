using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// Create or update collections request
    /// </summary>
    public class BoxCollectionsRequest
    {
        /// <summary>
        /// Gets or sets the collections.
        /// </summary>
        /// <value>
        /// The collections.
        /// </value>
        [JsonProperty(PropertyName = "collections")]
        public List<BoxRequestEntity> Collections { get; set; }
    }
}
