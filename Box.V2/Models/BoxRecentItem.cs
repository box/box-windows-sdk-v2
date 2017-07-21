using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Information about files that have been accessed by a user not long ago.
    /// </summary>
    public class BoxRecentItem : BoxEntity
    {
        /// <summary>
        /// The most recent type of access the user performed on the item. Possible values: 
        /// item_preview item_upload item_comment
        /// </summary>
        [JsonProperty(PropertyName = "interaction_type")]
        public string InteractionType { get; private set; }

        /// <summary>
        /// The time of the most recent interaction.
        /// </summary>
        [JsonProperty(PropertyName = "interacted_at")]
        public DateTime InteractedAt { get; private set; }

        /// <summary>
        /// If the item was accessed through a shared link it will appear here, otherwise this will be null.
        /// </summary>
        [JsonProperty(PropertyName = "interaction_shared_link")]
        public string InteractionSharedLink { get; protected set; }

        /// <summary>
        /// The item that was recently accessed.
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public BoxItem Item { get; protected set; }
    }
}
