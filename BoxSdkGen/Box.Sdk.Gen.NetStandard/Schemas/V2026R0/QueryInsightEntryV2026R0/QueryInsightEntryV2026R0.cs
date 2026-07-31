using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class QueryInsightEntryV2026R0 : ISerializable {
        /// <summary>
        /// The grouping key values associated with the entry. Contains one value per
        /// `group_by` field for `group` entries, and is empty for `overall` and
        /// `other` entries.
        /// </summary>
        [JsonPropertyName("key")]
        public IReadOnlyList<string> Key { get; set; }

        /// <summary>
        /// The type of insight entry, indicating how the associated metrics are
        /// aggregated.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<QueryInsightEntryV2026R0TypeField>))]
        public StringEnum<QueryInsightEntryV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// A map of metric aliases to their computed results. For `other` entries, the
        /// count is reported under the `totalCountBeyondTopGroups` key.
        /// </summary>
        [JsonPropertyName("metrics")]
        public Dictionary<string, QueryInsightMetricResultV2026R0> Metrics { get; set; }

        public QueryInsightEntryV2026R0(IReadOnlyList<string> key, QueryInsightEntryV2026R0TypeField type, Dictionary<string, QueryInsightMetricResultV2026R0> metrics) {
            Key = key;
            Type = type;
            Metrics = metrics;
        }
        
        [JsonConstructorAttribute]
        internal QueryInsightEntryV2026R0(IReadOnlyList<string> key, StringEnum<QueryInsightEntryV2026R0TypeField> type, Dictionary<string, QueryInsightMetricResultV2026R0> metrics) {
            Key = key;
            Type = type;
            Metrics = metrics;
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