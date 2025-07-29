using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class FileVersionBase : ISerializable {
        /// <summary>
        /// The unique identifier that represent a file version.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `file_version`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileVersionBaseTypeField>))]
        public StringEnum<FileVersionBaseTypeField> Type { get; }

        public FileVersionBase(string id, FileVersionBaseTypeField type = FileVersionBaseTypeField.FileVersion) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal FileVersionBase(string id, StringEnum<FileVersionBaseTypeField> type) {
            Id = id;
            Type = FileVersionBaseTypeField.FileVersion;
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