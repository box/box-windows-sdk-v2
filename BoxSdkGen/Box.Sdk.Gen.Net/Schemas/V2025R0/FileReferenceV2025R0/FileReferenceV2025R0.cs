using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class FileReferenceV2025R0 : ISerializable {
        /// <summary>
        /// The value will always be `file`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileReferenceV2025R0TypeField>))]
        public StringEnum<FileReferenceV2025R0TypeField> Type { get; }

        /// <summary>
        /// ID of the object.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public FileReferenceV2025R0(string id, FileReferenceV2025R0TypeField type = FileReferenceV2025R0TypeField.File) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal FileReferenceV2025R0(string id, StringEnum<FileReferenceV2025R0TypeField> type) {
            Type = FileReferenceV2025R0TypeField.File;
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