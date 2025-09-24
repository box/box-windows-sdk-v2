using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public const string FieldFilterFields = "filter_fields";
        public const string FieldStartDateField = "start_date_field";

        /// <summary>
        /// A mini retention policy object representing the retention policy that has been assigned to this content.
        /// </summary>
        [JsonProperty(PropertyName = FieldRetentionPolicy)]
        public virtual BoxRetentionPolicy RetentionPolicy { get; set; }

        /// <summary>
        /// The type and id of the content that is under retention. The type can either be folder or enterprise.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedTo)]
        public virtual BoxEntity AssignedTo { get; set; }

        /// <summary>
        /// A mini user object representing the user that created the retention policy assignment.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedBy)]
        public virtual BoxUser AssignedBy { get; set; }

        /// <summary>
        /// The time that the retention policy assignment was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedAt)]
        public virtual DateTimeOffset? AssignedAt { get; set; }

        /// <summary>
        /// Optional field filters for an assignment to a metadata template
        /// </summary>
        [JsonProperty(PropertyName = FieldFilterFields)]
        public virtual List<BoxMetadataFieldFilter> FilterFields { get; set; }

        /// <summary>
        /// Id of Metadata field which will be used to specify the start date for the retention policy.
        /// Alternatively, pass "upload_date" as value to use the date when the file was uploaded to Box.
        /// </summary>
        [JsonProperty(PropertyName = FieldStartDateField)]
        public virtual string StartDateField { get; set; }
    }
}
