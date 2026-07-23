using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class QueryInsightsRequestBodyV2026R0 : ISerializable {
        /// <summary>
        /// The filtering and grouping definition. Filters are applied first, followed
        /// by grouping, before metrics are computed.
        /// </summary>
        [JsonPropertyName("query")]
        public QueryInsightsRequestBodyV2026R0QueryField Query { get; set; }

        /// <summary>
        /// A map of user-defined metric aliases to their definitions. A maximum of 10
        /// metrics may be defined. Each alias must be a unique, non-empty string of up
        /// to 256 characters, containing only letters, digits, `_`, `-`, or `.`, and
        /// must not start with a digit, `_`, `-`, or `.`. May be empty to request
        /// only a total count.
        /// </summary>
        [JsonPropertyName("metrics")]
        public Dictionary<string, QueryInsightsMetricDefinitionV2026R0> Metrics { get; set; }

        public QueryInsightsRequestBodyV2026R0(QueryInsightsRequestBodyV2026R0QueryField query, Dictionary<string, QueryInsightsMetricDefinitionV2026R0> metrics) {
            Query = query;
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