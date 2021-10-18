using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// There is an inconsistency in the events API where file sources have slightly different field names.
    /// </summary>
    public class BoxUserFolderCollaborationEventSource : BoxEntity
    {
        public const string FieldFolderId = "folder_id";
        public const string FieldFolderName = "folder_name";
        public const string FieldUserId = "user_id";
        public const string FieldUserName = "user_name";
        public const string FieldParent = "parent";

        /// <summary>
        /// The unique ID of the folder being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderId)]
        public override string Id { get; protected set; }

        /// <summary>
        /// The type of the object.
        /// </summary>
        public override string Type { get { return "folder"; } protected set { return; } }

        /// <summary>
        /// The name of the folder  being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderName)]
        public string Name { get; private set; }

        /// <summary>
        /// The Id of the user collaborating on the folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldUserId)]
        public string UserId { get; private set; }

        /// <summary>
        /// The name of the user collaborating on the folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldUserName)]
        public string UserName { get; private set; }

        /// <summary>
        /// The parent folder of the folder being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}
