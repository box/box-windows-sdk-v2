using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// Copy of BoxFileEventSource
    /// </summary>
    public class BoxFolderEventSource : BoxEntity
    {
        public const string FieldItemType = "item_type";
        public const string FieldItemId = "item_id";
        public const string FieldItemName = "item_name";
        public const string FieldItemParent = "parent";
        public const string FieldOwnedBy = "owned_by";

        /// <summary>
        /// The type of the event source
        /// </summary>
        [JsonProperty(PropertyName = FieldItemType)]
        override public string Type { get; protected set; }

        /// <summary>
        /// The unique id of the file
        /// </summary>
        [JsonProperty(PropertyName = FieldItemId)]
        override public string Id { get; protected set; }

        /// <summary>
        /// The name of the file
        /// </summary>
        [JsonProperty(PropertyName = FieldItemName)]
        public string Name { get; private set; }

        /// <summary>
        /// The folder that contains this file
        /// </summary>
        [JsonProperty(PropertyName = FieldItemParent)]
        public BoxFolder Parent { get; private set; }

        /// <summary>
        /// The Box User that owns this source
        /// </summary>
        [JsonProperty(PropertyName = FieldOwnedBy)]
        public BoxUser OwnedBy { get; private set; }
    }
}
