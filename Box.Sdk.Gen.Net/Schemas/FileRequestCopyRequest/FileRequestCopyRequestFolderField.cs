using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileRequestCopyRequestFolderField : ISerializable {
        /// <summary>
        /// The value will always be `folder`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileRequestCopyRequestFolderTypeField>))]
        public StringEnum<FileRequestCopyRequestFolderTypeField>? Type { get; init; }

        /// <summary>
        /// The ID of the folder to associate the new
        /// file request to.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public FileRequestCopyRequestFolderField(string id) {
            Id = id;
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