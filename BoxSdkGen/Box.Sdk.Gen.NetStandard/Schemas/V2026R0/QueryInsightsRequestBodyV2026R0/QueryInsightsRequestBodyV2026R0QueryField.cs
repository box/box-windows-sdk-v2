using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class QueryInsightsRequestBodyV2026R0QueryField : ISerializable {
        /// <summary>
        /// A logical expression used to filter the dataset prior to metric
        /// computation, similar to an SQL `WHERE` clause. May include
        /// named parameters referenced as `:placeholder`.
        /// </summary>
        [JsonPropertyName("predicate")]
        public string Predicate { get; set; }

        /// <summary>
        /// A map of placeholder names (without the `:` prefix) to their values.
        /// Required only when the predicate contains parameter placeholders. The
        /// type of each value must match the type of the field it is compared to.
        /// </summary>
        [JsonPropertyName("params")]
        [JsonConverter(typeof(DictionaryObjectValuesConverter))]
        public Dictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// Restricts results to items contained within any of the specified
        /// ancestors. The user must have access to every listed ancestor. When
        /// omitted, insights are computed across all accessible items.
        /// </summary>
        [JsonPropertyName("ancestors")]
        public IReadOnlyList<QueryAncestorReferenceV2026R0> Ancestors { get; set; }

        /// <summary>
        /// Defines how data is grouped for insights computation. Currently only a
        /// single grouping field is supported.
        /// </summary>
        [JsonPropertyName("group_by")]
        public IReadOnlyList<QueryInsightsGroupByV2026R0> GroupBy { get; set; }

        public QueryInsightsRequestBodyV2026R0QueryField(string predicate) {
            Predicate = predicate;
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