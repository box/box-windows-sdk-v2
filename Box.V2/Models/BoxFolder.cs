using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a folder
    /// </summary>
    public class BoxFolder : BoxItem
    {
        
        public const string FieldFolderUploadEmail = "folder_upload_email";
        public const string FieldItemCollection = "item_collection";
        public const string FieldSyncState = "sync_state";

        /// <summary>
        /// The upload email address for this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderUploadEmail)]
        public BoxEmail FolderUploadEmail { get; private set; }

        /// <summary>
        /// A collection of mini file and folder objects contained in this folder
        /// </summary>
        [JsonProperty(PropertyName = FieldItemCollection)]
        public BoxCollection<BoxItem> ItemCollection { get; private set; }

        /// <summary>
        /// Whether this folder will be synced by the Box sync clients or not. Can be synced, not_synced, or partially_synced
        /// </summary>
        [JsonProperty(PropertyName = FieldSyncState)]
        public string SyncState { get; private set; }

    }
}
