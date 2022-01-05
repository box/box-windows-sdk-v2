using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a file version legal hold
    /// </summary>
    public class BoxFileVersionLegalHold : BoxEntity
    {
        public const string FieldFileVersion = "file_version";
        public const string FieldFile = "file";
        public const string FieldDeletedAt = "deleted_at";
        public const string FieldLegalHoldPolicyAssignments = "legal_hold_policy_assignments";

        /// <summary>
        /// The File-Version that is held.
        /// </summary>
        [JsonProperty(PropertyName = FieldFileVersion)]
        public virtual BoxFileVersion FileVersion { get; set; }

        /// <summary>
        /// The parent file of the File-Version that is held. Note that there is no guarantee that the current version of this File is held.
        /// </summary>
        [JsonProperty(PropertyName = FieldFile)]
        public virtual BoxFile File { get; set; }

        /// <summary>
        /// Time that this File-Version-Legal-Hold was deleted. If this is deleted, the file-version is not under Legal Hold.
        /// </summary>
        [JsonProperty(PropertyName = FieldDeletedAt)]
        public virtual DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// List of assignments contributing to this Hold.
        /// </summary>
        [JsonProperty(PropertyName = FieldLegalHoldPolicyAssignments)]
        public virtual BoxEntity[] LegalHoldPolicyAssignments { get; set; }
    }
}
