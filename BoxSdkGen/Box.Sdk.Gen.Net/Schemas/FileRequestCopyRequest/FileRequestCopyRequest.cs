using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileRequestCopyRequest : FileRequestUpdateRequest, ISerializable {
        /// <summary>
        /// The folder to associate the new file request to.
        /// </summary>
        [JsonPropertyName("folder")]
        public FileRequestCopyRequestFolderField Folder { get; }

        public FileRequestCopyRequest(FileRequestCopyRequestFolderField folder) {
            Folder = folder;
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}