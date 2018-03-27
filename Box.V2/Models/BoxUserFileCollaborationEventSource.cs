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
        public string FileId { get; private set; }

        [JsonProperty(PropertyName = FieldFileName)]
        public string FileName { get; private set; }

        [JsonProperty(PropertyName = FieldUserId)]
        public string UserId { get; private set; }

        [JsonProperty(PropertyName = FieldUserName)]
        public string UserName { get; private set; }

        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}