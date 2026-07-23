using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class QueryInsightMetricResultV2026R0 : ISerializable {
        /// <summary>
        /// The metric type that was computed.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The computed metric result(s), keyed by the metric function (for example
        /// `sum`, `avg`, `min`, `max`, or `count`).
        /// </summary>
        [JsonPropertyName("values")]
        public Dictionary<string, double> Values { get; set; }

        public QueryInsightMetricResultV2026R0(string type, Dictionary<string, double> values) {
            Type = type;
            Values = values;
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