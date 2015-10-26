using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a file version retention
    /// </summary>
    public class BoxFileVersionRetention : BoxEntity
    {
        public const string FieldFileVersion = "file_version";
        public const string FieldFile = "file";
        public const string FieldAppliedAt = "applied_at";
        public const string FieldDispositionAt = "disposition_at";
        public const string FieldWinningRetentionPolicy = "winning_retention_policy";

        /// <summary>
        /// The file version this file version retention was applied to.
        /// </summary>
        [JsonProperty(PropertyName = FieldFileVersion)]
        public BoxFileVersion FileVersion { get; set; }

        /// <summary>
        /// The file this file version retention was applied to.
        /// </summary>
        [JsonProperty(PropertyName = FieldFile)]
        public BoxFile File { get; set; }

        /// <summary>
        /// The time that this file version retention was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldAppliedAt)]
        public DateTime? AppliedAt { get; set; }

        /// <summary>
        /// The time that the retention period expires on this file version retention.
        /// </summary>
        [JsonProperty(PropertyName = FieldDispositionAt)]
        public DateTime? DispositionAt { get; set; }

        /// <summary>
        /// The winning retention policy applied to this file_version_retention. A file version can have multiple retention policies applied.
        /// </summary>
        [JsonProperty(PropertyName = FieldWinningRetentionPolicy)]
        public BoxRetentionPolicy WinningRetentionPolicy { get; set; }
    }
}
