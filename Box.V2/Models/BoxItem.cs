using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxItem : BoxEntity
    {
        /// <summary>
        /// A unique ID for use with the /events endpoint
        /// </summary>
        [JsonProperty(PropertyName = "sequence_id")]
        public string SequenceId { get; private set; }

        /// <summary>
        /// A unique string identifying the version of this item
        /// </summary>
        [JsonProperty(PropertyName = "etag")]
        public string ETag { get; private set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// The description of the item
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; private set; }

        /// <summary>
        /// The folder size in bytes
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long? Size { get; private set; }

        /// <summary>
        /// The path of folders to this item, starting at the root
        /// </summary>
        [JsonProperty(PropertyName = "path_collection")]
        public BoxCollection<BoxMiniFolder> PathCollection { get; private set; }

        /// <summary>
        /// The time the item was created
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The time the item or its contents were last modified
        /// </summary>
        [JsonProperty(PropertyName = "modified_at")]
        public DateTime? ModifiedAt { get; private set; }

        /// <summary>
        /// The user who created this item
        /// </summary>
        [JsonProperty(PropertyName = "created_by")]
        public BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The user who last modified this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = "modified_by")]
        public BoxUser ModifiedBy { get; private set; }

        /// <summary>
        /// The user who owns this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = "owned_by")]
        public BoxUser OwnedBy { get; private set; }

        /// <summary>
        /// The folder that contains this one
        /// </summary>
        [JsonProperty(PropertyName = "parent")]
        public BoxMiniFolder Parent { get; private set; }

        /// <summary>
        /// Whether this item is deleted or not
        /// </summary>
        [JsonProperty(PropertyName = "item_status")]
        public string ItemStatus { get; private set; }

        /// <summary>
        /// The shared link for this item
        /// </summary>
        [JsonProperty(PropertyName = "shared_link")]
        public BoxSharedLink SharedLink { get; private set; }
    }
}
