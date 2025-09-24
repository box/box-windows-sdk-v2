using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxLegalHoldPolicyAssignment : BoxEntity
    {
        public const string FieldLegalHoldPolicy = "legal_hold_policy";
        public const string FieldAssignedTo = "assigned_to";
        public const string FieldAssignedBy = "assigned_by";
        public const string FieldAssignedAt = "assigned_at";
        public const string FieldDeletedAt = "deleted_at";

        /// <summary>
        /// Gets the legal hold policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldLegalHoldPolicy)]
        public virtual BoxLegalHoldPolicy LegalHoldPolicy { get; private set; }

        /// <summary>
        /// Gets the user assigned to.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedTo)]
        public virtual BoxEntity AssignedTo { get; private set; }

        /// <summary>
        /// Gets the user assigned by.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedBy)]
        public virtual BoxUser AssignedBy { get; private set; }

        /// <summary>
        /// The time this legal hold policy was assigned
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedAt)]
        public virtual DateTimeOffset AssignedAt { get; private set; }

        /// <summary>
        /// The time this legal hold policy assignment was deleted
        /// </summary>
        [JsonProperty(PropertyName = FieldDeletedAt)]
        public virtual DateTimeOffset? DeletedAt { get; private set; }
    }
}
