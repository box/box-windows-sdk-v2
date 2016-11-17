using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    public class BoxLegalHoldPolicy : BoxEntity
    {
        public const string FieldPolicyName = "policy_name";
        public const string FieldDescription = "description";
        public const string FieldStatus = "status";
        public const string FieldCreatedBy = "created_by";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldDeletedAt = "deleted_at";
        public const string FieldFilterStartedAt = "filter_started_at";
        public const string FieldFilterEndedAt = "filter_ended_at";
        public const string FieldAssignmentCounts = "assignment_counts";

        /// <summary>
        /// Name of Legal Hold Policy. Max characters 254.
        /// </summary>
        [JsonProperty(PropertyName = FieldPolicyName)]
        public string PolicyName { get; private set; }

        /// <summary>
        /// Description of Legal Hold Policy. Max characters 500.
        /// </summary>
        [JsonProperty(PropertyName = FieldDescription)]
        public string Description { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public string Status { get; private set; }

        /// <summary>
        /// A user object representing the author of the legal hold policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The time this legal hold policy was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The time this legal hold policy was last modified.
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; private set; }

        /// <summary>
        /// The time this legal hold policy was deleted.
        /// </summary>
        [JsonProperty(PropertyName = FieldDeletedAt)]
        public DateTime? DeletedAt { get; private set; }

        /// <summary>
        /// Date filter applies to Custodian assignments only.
        /// </summary>
        [JsonProperty(PropertyName = FieldFilterStartedAt)]
        public DateTime? FilterStartedAt { get; private set; }

        /// <summary>
        /// Date filter applies to Custodian assignments only.
        /// </summary>
        [JsonProperty(PropertyName = FieldFilterEndedAt)]
        public DateTime? FilterEndedAt { get; private set; }

        /// <summary>
        /// Gets the assignment counts.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignmentCounts)]
        public BoxAssignmentCounts AssignmentCounts { get; private set; }
    }
}
