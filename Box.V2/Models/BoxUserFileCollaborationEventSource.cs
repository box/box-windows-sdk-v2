using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxUserFileCollaborationEventSource : BoxEntity
    {
        public const string FieldFileId = "file_id";
        public const string FieldFileName = "file_name";
        public const string FieldUserId = "user_id";
        public const string FieldUserName = "user_name";
        public const string FieldParent = "parent";

        [JsonProperty(PropertyName = FieldFileId)]
        new public string FileId { get; private set; }

        [JsonProperty(PropertyName = FieldFileName)]
        new public string FileName { get; private set; }

        [JsonProperty(PropertyName = FieldUserId)]
        new public string GroupId { get; private set; }

        [JsonProperty(PropertyName = FieldUserName)]
        new public string GroupName { get; private set; }

        [JsonProperty(PropertyName = FieldParent)]
        new public BoxFolder Parent { get; private set; }
    }
}