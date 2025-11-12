using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ArchiveV2025R0OwnedByField : ISerializable {
        /// <summary>
        /// The unique identifier that represents a user who owns the archive.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value is always `user`.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; }

        public ArchiveV2025R0OwnedByField(string id, string type) {
            Id = id;
            Type = type;
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