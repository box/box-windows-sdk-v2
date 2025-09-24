using System;
using Newtonsoft.Json;

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
        public const string FieldIsOngoing = "is_ongoing";
        public const string FieldAssignmentCounts = "assignment_counts";

        /// <summary>
        /// Name of Legal Hold Policy. Max characters 254.
        /// </summary>
        [JsonProperty(PropertyName = FieldPolicyName)]
        public virtual string PolicyName { get; private set; }

        /// <summary>
        /// Description of Legal Hold Policy. Max characters 500.
        /// </summary>
        [JsonProperty(PropertyName = FieldDescription)]
        public virtual string Description { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public virtual string Status { get; private set; }

        /// <summary>
        /// A user object representing the author of the legal hold policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time this legal hold policy was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The time this legal hold policy was last modified.
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }

        /// <summary>
        /// The time this legal hold policy was deleted.
        /// </summary>
        [JsonProperty(PropertyName = FieldDeletedAt)]
        public virtual DateTimeOffset? DeletedAt { get; private set; }

        /// <summary>
        /// Date filter applies to Custodian assignments only.
        /// </summary>
        [JsonProperty(PropertyName = FieldFilterStartedAt)]
        public virtual DateTimeOffset? FilterStartedAt { get; private set; }

        /// <summary>
        /// Date filter applies to Custodian assignments only.
        /// </summary>
        [JsonProperty(PropertyName = FieldFilterEndedAt)]
        public virtual DateTimeOffset? FilterEndedAt { get; private set; }

        /// <summary>
        /// Gets the assignment counts.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignmentCounts)]
        public virtual BoxAssignmentCounts AssignmentCounts { get; private set; }

        /// <summary>
        /// Whether this assignment will continue applying to File Versions even after initialization.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsOngoing)]
        public virtual bool IsOngoing { get; private set; }
    }
}
