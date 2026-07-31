using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class QueryInsightsGroupByV2026R0 : ISerializable {
        /// <summary>
        /// The fully qualified field name to group by. Supports metadata and item
        /// properties.
        /// </summary>
        [JsonPropertyName("field")]
        public string Field { get; }

        /// <summary>
        /// The maximum number of buckets to return for the grouping. Defaults to `5`.
        /// </summary>
        [JsonPropertyName("bucket_limit")]
        public int? BucketLimit { get; init; }

        public QueryInsightsGroupByV2026R0(string field) {
            Field = field;
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