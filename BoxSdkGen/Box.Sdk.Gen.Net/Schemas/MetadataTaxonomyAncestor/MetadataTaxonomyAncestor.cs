using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataTaxonomyAncestor : ISerializable {
        /// <summary>
        /// A unique identifier of the metadata taxonomy node.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The display name of the metadata taxonomy node.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; init; }

        /// <summary>
        /// An index of the level to which the node belongs.
        /// </summary>
        [JsonPropertyName("level")]
        public long? Level { get; init; }

        public MetadataTaxonomyAncestor() {
            
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