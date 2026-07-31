using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class QueryRequestBodyV2026R0QueryField : ISerializable {
        /// <summary>
        /// A logical expression used to filter the dataset, similar to an SQL
        /// `WHERE` clause. May include named parameters referenced as
        /// `:placeholder`.
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
        /// Restricts results to the specified ancestor entities and their
        /// recursive descendants. The user must have read access to every listed
        /// ancestor.
        /// </summary>
        [JsonPropertyName("ancestors")]
        public IReadOnlyList<QueryAncestorReferenceV2026R0> Ancestors { get; set; }

        public QueryRequestBodyV2026R0QueryField(string predicate) {
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