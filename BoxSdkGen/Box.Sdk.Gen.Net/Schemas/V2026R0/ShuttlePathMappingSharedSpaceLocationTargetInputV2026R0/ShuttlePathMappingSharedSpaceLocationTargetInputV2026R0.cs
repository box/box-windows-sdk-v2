using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0 : ISerializable {
        /// <summary>
        /// The value will always be `shared_space`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0TypeField>))]
        public StringEnum<ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0TypeField> Type { get; }

        /// <summary>
        /// The display name of the shared container. Required for systems with
        /// multiple containers.
        /// </summary>
        [JsonPropertyName("space")]
        public string? Space { get; init; }

        /// <summary>
        /// The human-readable path relative to the shared container.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; }

        /// <summary>
        /// An optional stable identifier of the location, when obtained from
        /// browsing.
        /// </summary>
        [JsonPropertyName("path_id")]
        public string? PathId { get; init; }

        public ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0(string path, ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0TypeField type = ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0TypeField.SharedSpace) {
            Type = type;
            Path = path;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0(string path, StringEnum<ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0TypeField> type) {
            Type = ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0TypeField.SharedSpace;
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