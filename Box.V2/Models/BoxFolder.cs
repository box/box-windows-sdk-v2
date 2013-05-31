using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxFolder : BoxItem
    {
        /// <summary>
        /// The upload email address for this folder
        /// </summary>
        [JsonProperty(PropertyName = "folder_upload_email")]
        public BoxEmail FolderUploadEmail { get; private set; }

        /// <summary>
        /// A collection of mini file and folder objects contained in this folder
        /// </summary>
        [JsonProperty(PropertyName = "item_collection")]
        public BoxCollection<BoxFile> ItemCollection { get; private set; }

        /// <summary>
        /// Whether this folder will be synced by the Box sync clients or not. Can be synced, not_synced, or partially_synced
        /// </summary>
        [JsonProperty(PropertyName = "sync_state")]
        public string SyncState { get; private set; }

    }
}
