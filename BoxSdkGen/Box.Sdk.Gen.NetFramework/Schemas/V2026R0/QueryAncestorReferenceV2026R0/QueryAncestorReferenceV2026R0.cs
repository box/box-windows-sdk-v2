using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class QueryAncestorReferenceV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier of the ancestor entity.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of the ancestor entity. Possible value: folder.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        public QueryAncestorReferenceV2026R0(string id, string type) {
            Id = id;
            Type = type;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}