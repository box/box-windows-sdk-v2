using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxSearchResult
    {
        /// <summary>
        /// The type of the item
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public virtual string Type { get; private set; }

        /// <summary>
        /// The optional shared link through which the user has access to this item.
        /// This value is only returned for items for which the user has recently accessed the file through a shared link. For all other items this value will return null.
        /// </summary>
        [JsonProperty(PropertyName = "accessible_via_shared_link")]
        public virtual Uri AccessibleViaSharedLink { get; private set; }

        /// <summary>
        /// The Box item
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public virtual BoxItem Item { get; private set; }
    }
}
