using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ArchiveV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier that represents an archive.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `archive`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ArchiveV2025R0TypeField>))]
        public StringEnum<ArchiveV2025R0TypeField> Type { get; }

        /// <summary>
        /// The name of the archive.
        /// 
        /// The following restrictions to the archive name apply: names containing
        /// non-printable ASCII characters, forward and backward slashes
        /// (`/`, `\`), names with trailing spaces, and names `.` and `..` are
        /// not allowed.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// The size of the archive in bytes.
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; }

        public ArchiveV2025R0(string id, string name, long size, ArchiveV2025R0TypeField type = ArchiveV2025R0TypeField.Archive) {
            Id = id;
            Type = type;
            Name = name;
            Size = size;
        }
        
        [JsonConstructorAttribute]
        internal ArchiveV2025R0(string id, string name, long size, StringEnum<ArchiveV2025R0TypeField> type) {
            Id = id;
            Type = ArchiveV2025R0TypeField.Archive;
            Name = name;
            Size = size;
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