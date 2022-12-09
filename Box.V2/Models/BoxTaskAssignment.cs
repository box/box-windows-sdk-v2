using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    ///  Assignment for a given task
    /// </summary>
    public class BoxTaskAssignment : BoxEntity
    {
        public const string FieldItem = "item";
        public const string FieldAssignedTo = "assigned_to";
        public const string FieldMessage = "message";
        public const string FieldCompletedAt = "completed_at";
        public const string FieldAssignedAt = "assigned_at";
        public const string FieldRemindedAt = "reminded_at";
        public const string FieldResolutionState = "resolution_state";
        public const string FieldStatus = "status";
        public const string FieldAssignedBy = "assigned_by";

        /// <summary>
        /// Gets assigned item 
        /// </summary>
        [JsonProperty(PropertyName = FieldItem)]
        public virtual BoxItem Item { get; private set; }

        /// <summary>
        /// Gets user assigned to.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedTo)]
        public virtual BoxUser AssignedTo { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        [JsonProperty(PropertyName = FieldMessage)]
        public virtual string Message { get; private set; }

        /// <summary>
        /// Completed at.
        /// </summary>
        [JsonProperty(PropertyName = FieldCompletedAt)]
        public virtual DateTimeOffset? CompletedAt { get; private set; }

        /// <summary>
        /// Assigned at.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedAt)]
        public virtual DateTimeOffset? AssignedAt { get; private set; }

        /// <summary>
        /// Reminded at.
        /// </summary>
        [JsonProperty(PropertyName = FieldRemindedAt)]
        public virtual DateTimeOffset? RemindedAt { get; private set; }

        /// <summary>
        /// Gets the state of the resolution as an Enum.
        /// </summary>
        public virtual ResolutionStateType? ResolutionState
        {
            get
            {
                return (ResolutionStateType)Enum.Parse(typeof(ResolutionStateType), LocalizedStatus, ignoreCase: true);
            }
        }

        /// <summary>
        /// Gets the resolution status of the task assignment.
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public virtual string Status { get; private set; }

        /// <summary>
        /// Gets the localized/human-readable resolution status of the task assignment in a string format.
        /// </summary>
        [JsonProperty(PropertyName = FieldResolutionState)]
        public virtual string LocalizedStatus { get; private set; }

        /// <summary>
        ///Gets user assigned by.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedBy)]
        public virtual BoxUser AssignedBy { get; private set; }
    }
}
