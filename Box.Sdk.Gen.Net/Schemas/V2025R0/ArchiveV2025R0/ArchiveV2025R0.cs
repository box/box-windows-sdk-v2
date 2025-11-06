using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ArchiveV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdescriptionSet")]
        protected bool _isDescriptionSet { get; set; }

        protected string? _description { get; set; }

        /// <summary>
        /// The unique identifier that represents an archive.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value is always `archive`.
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

        /// <summary>
        /// The description of the archive.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get => _description; init { _description = value; _isDescriptionSet = true; } }

        /// <summary>
        /// The part of an archive API response that describes the user who owns the archive.
        /// </summary>
        [JsonPropertyName("owned_by")]
        public ArchiveV2025R0OwnedByField? OwnedBy { get; init; }

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