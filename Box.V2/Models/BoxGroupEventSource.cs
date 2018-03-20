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
        new public string GroupId { get; private set; }

        /// <summary>
        /// The name of the group resource. 
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupName)]
        new public string GroupName { get; private set; }
    }
}
