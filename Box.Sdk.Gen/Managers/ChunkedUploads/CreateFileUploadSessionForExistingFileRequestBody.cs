using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateFileUploadSessionForExistingFileRequestBody : ISerializable {
        /// <summary>
        /// The total number of bytes of the file to be uploaded.
        /// </summary>
        [JsonPropertyName("file_size")]
        public long FileSize { get; }

        /// <summary>
        /// The optional new name of new file.
        /// </summary>
        [JsonPropertyName("file_name")]
        public string? FileName { get; init; }

        public CreateFileUploadSessionForExistingFileRequestBody(long fileSize) {
            FileSize = fileSize;
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