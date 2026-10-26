using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingAccountLocationV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_ispath_idSet")]
        protected bool _isPathIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isis_directorySet")]
        protected bool _isIsDirectorySet { get; set; }

        protected string? _pathId { get; set; }

        protected bool? _isDirectory { get; set; }

        /// <summary>
        /// The value will always be `account`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathMappingAccountLocationV2026R0TypeField>))]
        public StringEnum<ShuttlePathMappingAccountLocationV2026R0TypeField> Type { get; }

        /// <summary>
        /// The human-readable path relative to the account root.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; }

        /// <summary>
        /// The stable identifier of the location. `null` when the system does not
        /// provide one.
        /// </summary>
        [JsonPropertyName("path_id")]
        public string? PathId { get => _pathId; init { _pathId = value; _isPathIdSet = true; } }

        [JsonPropertyName("account")]
        public ShuttlePathMappingAccountV2026R0? Account { get; init; }

        /// <summary>
        /// Whether the location is a directory. `null` when unknown.
        /// </summary>
        [JsonPropertyName("is_directory")]
        public bool? IsDirectory { get => _isDirectory; init { _isDirectory = value; _isIsDirectorySet = true; } }

        public ShuttlePathMappingAccountLocationV2026R0(string path, ShuttlePathMappingAccountLocationV2026R0TypeField type = ShuttlePathMappingAccountLocationV2026R0TypeField.Account) {
            Type = type;
            Path = path;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathMappingAccountLocationV2026R0(string path, StringEnum<ShuttlePathMappingAccountLocationV2026R0TypeField> type) {
            Type = ShuttlePathMappingAccountLocationV2026R0TypeField.Account;
            Path = path;
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