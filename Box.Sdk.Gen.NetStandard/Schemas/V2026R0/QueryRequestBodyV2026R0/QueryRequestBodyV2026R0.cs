using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class QueryRequestBodyV2026R0 : ISerializable {
        /// <summary>
        /// The query definition, including the filtering predicate and its optional
        /// parameters and ancestor restrictions.
        /// </summary>
        [JsonPropertyName("query")]
        public QueryRequestBodyV2026R0QueryField Query { get; set; }

        /// <summary>
        /// The sorting criteria for the result set. Entries are applied sequentially
        /// to define multi-level sorting.
        /// </summary>
        [JsonPropertyName("order_by")]
        public IReadOnlyList<QueryOrderByV2026R0> OrderBy { get; set; }

        /// <summary>
        /// The maximum number of results to return. Defaults to `50` when not
        /// provided.
        /// </summary>
        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Controls which additional fields are included in each result entry. Each
        /// value must be one of: a fully qualified item field key (for example
        /// `box:item:name`), a metadata template key to hydrate the full template (for
        /// example `enterprise_12345678:project`), or a specific metadata template
        /// field key to hydrate a single field from the template (for example
        /// `enterprise_12345678:project:name`). When omitted, entries include only the
        /// item type and identifier.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<string> Fields { get; set; }

        /// <summary>
        /// An opaque token returned from a previous response, used to continue
        /// retrieval. When provided, all other request parameters must exactly match
        /// those of the original request.
        /// </summary>
        [JsonPropertyName("marker")]
        public string Marker { get; set; }

        public QueryRequestBodyV2026R0(QueryRequestBodyV2026R0QueryField query) {
            Query = query;
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