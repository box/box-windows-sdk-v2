using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxGroupFileCollaborationEventSource : BoxEntity
    {
        public const string FieldFileId = "file_id";
        public const string FieldFileName = "file_name";
        public const string FieldGroupId = "group_id";
        public const string FieldGroupName = "group_name";
        public const string FieldParent = "parent";

        [JsonProperty(PropertyName = FieldFileId)]
        public string Id { get; private set; }

        public override string Type { get { return "file"; } protected set { return; } }

        [JsonProperty(PropertyName = FieldFileName)]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = FieldGroupId)]
        public string GroupId { get; private set; }

        [JsonProperty(PropertyName = FieldGroupName)]
        public string GroupName { get; private set; }

        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}
