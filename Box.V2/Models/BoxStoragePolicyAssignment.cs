using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxStoragePolicyAssignment : BoxEntity
    {
        public const string FieldStoragePolicy = "storage_policy";
        public const string FieldAssignedTo = "assigned_to";

        /// <summary>
        /// The storage policy to assign to user.
        /// </summary>
        [JsonProperty(PropertyName = FieldStoragePolicy)]
        public BoxEntity StoragePolicy { get; set; }

        /// <summary>
        /// The Box User to assign the storage policy to.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedTo)]
        public BoxUser AssignedTo { get; set; }
    }
}
