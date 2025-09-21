using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataFieldFilterFloatRange : ISerializable {
        /// <summary>
        /// Specifies the (inclusive) upper bound for the metadata field
        /// value. The value of a field must be lower than (`lt`) or
        /// equal to this value for the search query to match this
        /// template.
        /// </summary>
        [JsonPropertyName("lt")]
        public double? Lt { get; init; }

        /// <summary>
        /// Specifies the (inclusive) lower bound for the metadata field
        /// value. The value of a field must be greater than (`gt`) or
        /// equal to this value for the search query to match this
        /// template.
        /// </summary>
        [JsonPropertyName("gt")]
        public double? Gt { get; init; }

        public MetadataFieldFilterFloatRange() {
            
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