using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// Box representation of the status of a download for a zip file
    /// </summary>
    public class BoxZipDownloadStatus
    {
        public const string FieldTotalFileCount = "total_file_count";
        public const string FieldDownloadedFileCount = "downloaded_file_count";
        public const string FieldSkippedFileCount = "skipped_file_count";
        public const string FieldSkippedFolderCount = "skipped_folder_count";
        public const string FieldState = "state";

        /// <summary>
        /// The total number of files in the zip file
        /// </summary>
        [JsonProperty(PropertyName = FieldTotalFileCount)]
        public virtual int TotalFileCount { get; set; }

        /// <summary>
        /// The number of files in the zip that were downloaded
        /// </summary>
        [JsonProperty(PropertyName = FieldDownloadedFileCount)]
        public virtual int DownloadedlFileCount { get; set; }

        /// <summary>
        /// The number of files in the zip that were skipped when downloading
        /// </summary>
        [JsonProperty(PropertyName = FieldSkippedFileCount)]
        public virtual int SkippedFileCount { get; set; }

        /// <summary>
        /// The number of folders in the zip that were skipped when downloading
        /// </summary>
        [JsonProperty(PropertyName = FieldSkippedFolderCount)]
        public virtual int SkippedFolderCount { get; set; }

        /// <summary>
        /// The state of the download of the zip file
        /// </summary>
        [JsonProperty(PropertyName = FieldState)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual BoxZipDownloadState State { get; set; }

        /// <summary>
        /// A list of naming conflicts among the files and folders in the zip. This is manually appended in the BoxFilesManager.DownloadZip() method.
        /// </summary>
        public virtual List<BoxZipConflict> NameConflicts { get; set; }
    }

    /// <summary>
    /// The possible download states of a zip
    /// </summary>
    public enum BoxZipDownloadState
    {
        succeeded,
        in_progress,
        failed
    }
}
