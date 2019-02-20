using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// There is an inconsistency in the events API where group sources have slightly different field names.
    /// </summary>
    public class BoxGroupEventSource : BoxEntity
    {
        public const string FieldGroupId = "group_id";
        public const string FieldGroupName = "group_name";

        /// <summary>
        /// The unique id of the group resource.
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupId)]
        public override string Id { get; protected set; }

        /// <summary>
        /// The type of the object.
        /// </summary>
        public override string Type { get { return "group"; } protected set { return; } }

        /// <summary>
        /// The name of the group resource. 
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupName)]
        public string Name { get; private set; }
    }
}
