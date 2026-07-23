using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class QueryInsightsV2026R0 : ISerializable {
        /// <summary>
        /// The list of computed insight entries. Each entry corresponds to a group,
        /// the overall dataset, or the aggregate of groups outside the top results.
        /// </summary>
        [JsonPropertyName("insights")]
        public IReadOnlyList<QueryInsightEntryV2026R0> Insights { get; set; }

        public QueryInsightsV2026R0(IReadOnlyList<QueryInsightEntryV2026R0> insights) {
            Insights = insights;
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