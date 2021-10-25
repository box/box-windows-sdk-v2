using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxExternalUserFileCollaborationEventSource : BoxEntity
    {
        /// <summary>
        /// The unique ID of the file being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = "file_id")]
        public override string Id { get; protected set; }
        
        /// <summary>
        /// The type of the object.
        /// </summary>
        public override string Type { get => "file"; protected set { } }
        
        /// <summary>
        /// The name of the file being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = "file_name")]
        public string Name { get; private set; }
        
        /// <summary>
        /// The email of the user collaborating on the file.
        /// </summary>
        [JsonProperty(PropertyName = "user_email")]
        public string UserEmail { get; private set; }
        
        /// <summary>
        /// The parent folder of the file being collaborated on.
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
