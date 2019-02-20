using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxGroupFolderCollaborationEventSource : BoxEntity
    {
        public const string FieldFolderId = "folder_id";
        public const string FieldFolderName = "folder_name";
        public const string FieldGroupId = "group_id";
        public const string FieldGroupName = "group_name";
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
        /// The name of the folder being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderName)]
        public string Name { get; private set; }

        /// <summary>
        /// The unique ID of the group collaborating on the folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupId)]
        public string GroupId { get; private set; }

        /// <summary>
        /// The name of the group collaborating on the folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupName)]
        public string GroupName { get; private set; }

        /// <summary>
        /// The parent folder of the folder being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}
