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

        [JsonProperty(PropertyName = FieldFolderId)]
        public string FolderId { get; private set; }

        [JsonProperty(PropertyName = FieldFolderName)]
        public string FolderName { get; private set; }

        [JsonProperty(PropertyName = FieldGroupId)]
        public string GroupId { get; private set; }

        [JsonProperty(PropertyName = FieldGroupName)]
        public string GroupName { get; private set; }

        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}
