using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ZipDownloadStatus : ISerializable {
        /// <summary>
        /// The total number of files in the archive.
        /// </summary>
        [JsonPropertyName("total_file_count")]
        public long? TotalFileCount { get; init; }

        /// <summary>
        /// The number of files that have already been downloaded.
        /// </summary>
        [JsonPropertyName("downloaded_file_count")]
        public long? DownloadedFileCount { get; init; }

        /// <summary>
        /// The number of files that have been skipped as they could not be
        /// downloaded. In many cases this is due to permission issues that have
        /// surfaced between the creation of the request for the archive and the
        /// archive being downloaded.
        /// </summary>
        [JsonPropertyName("skipped_file_count")]
        public long? SkippedFileCount { get; init; }

        /// <summary>
        /// The number of folders that have been skipped as they could not be
        /// downloaded. In many cases this is due to permission issues that have
        /// surfaced between the creation of the request for the archive and the
        /// archive being downloaded.
        /// </summary>
        [JsonPropertyName("skipped_folder_count")]
        public long? SkippedFolderCount { get; init; }

        /// <summary>
        /// The state of the archive being downloaded.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter<ZipDownloadStatusStateField>))]
        public StringEnum<ZipDownloadStatusStateField>? State { get; init; }

        public ZipDownloadStatus() {
            
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}