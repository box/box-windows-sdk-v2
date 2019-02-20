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
        /// The unique ID of the user collaborating on the file.
        /// </summary>
        [JsonProperty(PropertyName = FieldUserId)]
        public string UserId { get; private set; }

        /// <summary>
        /// The name of the user collaborating on the file.
        /// </summary>
        [JsonProperty(PropertyName = FieldUserName)]
        public string UserName { get; private set; }

        /// <summary>
        /// The parent folder of the file being collaborated on.
        /// </summary>
        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}
