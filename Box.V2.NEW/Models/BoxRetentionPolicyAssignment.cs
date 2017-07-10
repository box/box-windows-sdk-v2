using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a retention assignment
    /// </summary>
    public class BoxRetentionPolicyAssignment : BoxEntity
    {
        public const string FieldRetentionPolicy = "retention_policy";
        public const string FieldAssignedTo = "assigned_to";
        public const string FieldAssignedBy = "assigned_by";
        public const string FieldAssignedAt = "assigned_at";

        /// <summary>
        /// A mini retention policy object representing the retention policy that has been assigned to this content.
        /// </summary>
        [JsonProperty(PropertyName = FieldRetentionPolicy)]
        public BoxRetentionPolicy RetentionPolicy { get; set; }

        /// <summary>
        /// The type and id of the content that is under retention. The type can either be folder or enterprise.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedTo)]
        public BoxEntity AssignedTo { get; set; }

        /// <summary>
        /// A mini user object representing the user that created the retention policy assignment.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedBy)]
        public BoxUser AssignedBy { get; set; }

        /// <summary>
        /// The time that the retention policy assignment was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedAt)]
        public DateTime? AssignedAt { get; set; }

    }
}
