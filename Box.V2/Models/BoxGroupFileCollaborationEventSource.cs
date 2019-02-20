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

        /// <summary>
        /// The unique ID of the file being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldFileId)]
        public override string Id { get; protected set; }

        /// <summary>
        /// The type of the object.
        /// </summary>
        public override string Type { get { return "file"; } protected set { return; } }

        /// <summary>
        /// The name of the file being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldFileName)]
        public string Name { get; private set; }

        /// <summary>
        /// The unique ID of the group collaborating on the file.
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupId)]
        public string GroupId { get; private set; }

        /// <summary>
        /// The name of the group collaborating on the file.
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupName)]
        public string GroupName { get; private set; }

        /// <summary>
        /// The parent folder of the file being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}
