using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxExternalUserFolderCollaborationEventSource : BoxEntity
    {
        /// <summary>
        /// The unique ID of the folder being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = "folder_id")]
        public override string Id { get; protected set; }
        
        /// <summary>
        /// The type of the object.
        /// </summary>
        public override string Type { get => "folder"; protected set { } }
        
        /// <summary>
        /// The name of the folder  being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = "folder_name")]
        public string Name { get; private set; }
        
        /// <summary>
        /// The email of the user collaborating on the folder.
        /// </summary>
        [JsonProperty(PropertyName = "user_email")]
        public string UserEmail { get; private set; }

        /// <summary>
        /// The parent folder of the folder being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = "parent")]
        public BoxFolder Parent { get; private set; }
        
        /// <summary>
        /// The Box User that owns this source.
        /// </summary>
        [JsonProperty(PropertyName = "owned_by")]
        public BoxUser OwnedBy { get; private set; }
    }
}
