using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class QueryInsightsMetricDefinitionV2026R0 : ISerializable {
        /// <summary>
        /// The aggregation function to apply.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<QueryInsightsMetricDefinitionV2026R0TypeField>))]
        public StringEnum<QueryInsightsMetricDefinitionV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The fully qualified field name on which the metric is computed.
        /// </summary>
        [JsonPropertyName("field")]
        public string Field { get; set; }

        public QueryInsightsMetricDefinitionV2026R0(QueryInsightsMetricDefinitionV2026R0TypeField type, string field) {
            Type = type;
            Field = field;
        }
        
        [JsonConstructorAttribute]
        internal QueryInsightsMetricDefinitionV2026R0(StringEnum<QueryInsightsMetricDefinitionV2026R0TypeField> type, string field) {
            Type = type;
            Field = field;
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