using Box.V2.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a created zip
    /// </summary>
    public class BoxZip
    {
        public const string FieldDownloadUrl = "download_url";
        public const string FieldStatusUrl = "status_url";
        public const string FieldExpiresAt = "expires_at";
        public const string FieldNameConflicts = "name_conflicts";

        /// <summary>
        /// A URL to download the zip from
        /// </summary>
        [JsonProperty(PropertyName = FieldDownloadUrl)]
        public Uri DownloadUrl { get; private set; }

        /// <summary>
        /// A URL to get the download status of a zip
        /// </summary>
        [JsonProperty(PropertyName = FieldStatusUrl)]
        public Uri StatusUrl { get; private set; }

        /// <summary>
        /// The date after which the zip can no longer be downloaded
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public DateTime ExpiresAt { get; private set; }

        /// <summary>
        /// A list of naming conflicts among the files and folders in the zip
        /// </summary>
        [JsonProperty(PropertyName = FieldNameConflicts)]
        [JsonConverter(typeof(BoxZipConflictConverter))]
        public List<BoxZipConflict> NameConflicts { get; private set; }
    }
}

