using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingSharedSpaceLocationV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isspaceSet")]
        protected bool _isSpaceSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ispath_idSet")]
        protected bool _isPathIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isis_directorySet")]
        protected bool _isIsDirectorySet { get; set; }

        protected string? _space { get; set; }

        protected string? _pathId { get; set; }

        protected bool? _isDirectory { get; set; }

        /// <summary>
        /// The value will always be `shared_space`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathMappingSharedSpaceLocationV2026R0TypeField>))]
        public StringEnum<ShuttlePathMappingSharedSpaceLocationV2026R0TypeField> Type { get; }

        /// <summary>
        /// The display name of the shared container. `null` when the system has a
        /// single implicit container.
        /// </summary>
        [JsonPropertyName("space")]
        public string? Space { get => _space; init { _space = value; _isSpaceSet = true; } }

        /// <summary>
        /// The human-readable path relative to the shared container.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; }

        /// <summary>
        /// The stable identifier of the location. `null` when the system does not
        /// provide one.
        /// </summary>
        [JsonPropertyName("path_id")]
        public string? PathId { get => _pathId; init { _pathId = value; _isPathIdSet = true; } }

        /// <summary>
        /// Whether the location is a directory. `null` when unknown.
        /// </summary>
        [JsonPropertyName("is_directory")]
        public bool? IsDirectory { get => _isDirectory; init { _isDirectory = value; _isIsDirectorySet = true; } }

        public ShuttlePathMappingSharedSpaceLocationV2026R0(string path, ShuttlePathMappingSharedSpaceLocationV2026R0TypeField type = ShuttlePathMappingSharedSpaceLocationV2026R0TypeField.SharedSpace) {
            Type = type;
            Path = path;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathMappingSharedSpaceLocationV2026R0(string path, StringEnum<ShuttlePathMappingSharedSpaceLocationV2026R0TypeField> type) {
            Type = ShuttlePathMappingSharedSpaceLocationV2026R0TypeField.SharedSpace;
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