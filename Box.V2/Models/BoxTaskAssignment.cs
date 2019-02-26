using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

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
        public BoxItem Item { get; private set; }

        /// <summary>
        /// Gets user assigned to.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedTo)]
        public BoxUser AssignedTo { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        [JsonProperty(PropertyName = FieldMessage)]
        public string Message { get; private set; }

        /// <summary>
        /// Completed at.
        /// </summary>
        [JsonProperty(PropertyName = FieldCompletedAt)]
        public DateTime? CompletedAt { get; private set; }

        /// <summary>
        /// Assigned at.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedAt)]
        public DateTime? AssignedAt { get; private set; }

        /// <summary>
        /// Reminded at.
        /// </summary>
        [JsonProperty(PropertyName = FieldRemindedAt)]
        public DateTime? RemindedAt { get; private set; }

        /// <summary>
        /// Gets the state of the resolution.
        /// </summary>
        [Obsolete("This field is deprecated, and may not work consistently.  Use Status or LocalizedStatus instead.")]
        public ResolutionStateType? ResolutionState {
            get
            {
                return (ResolutionStateType) System.Enum.Parse(typeof(ResolutionStateType), Status, ignoreCase: true);
            }
        }

        /// <summary>
        /// Gets the resolution status of the task assignment.
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public string Status { get; private set; }

        /// <summary>
        /// Gets the localized/human-readable resolution status of the task assignment.
        /// </summary>
        [JsonProperty(PropertyName = FieldResolutionState)]
        public string LocalizedStatus { get; private set; }

        /// <summary>
        ///Gets user assigned by.
        /// </summary>
        [JsonProperty(PropertyName = FieldAssignedBy)]
        public BoxUser AssignedBy { get; private set; }
    }
}
