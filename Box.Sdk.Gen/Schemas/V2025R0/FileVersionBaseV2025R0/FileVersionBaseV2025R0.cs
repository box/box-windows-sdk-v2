using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class FileVersionBaseV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier that represent a file version.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `file_version`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileVersionBaseV2025R0TypeField>))]
        public StringEnum<FileVersionBaseV2025R0TypeField> Type { get; }

        public FileVersionBaseV2025R0(string id, FileVersionBaseV2025R0TypeField type = FileVersionBaseV2025R0TypeField.FileVersion) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal FileVersionBaseV2025R0(string id, StringEnum<FileVersionBaseV2025R0TypeField> type) {
            Id = id;
            Type = FileVersionBaseV2025R0TypeField.FileVersion;
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