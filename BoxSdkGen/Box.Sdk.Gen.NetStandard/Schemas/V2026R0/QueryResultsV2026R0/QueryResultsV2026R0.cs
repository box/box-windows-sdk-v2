using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class QueryResultsV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnext_markerSet")]
        protected bool _isNextMarkerSet { get; set; }

        protected string _nextMarker { get; set; }

        /// <summary>
        /// The list of items matching the query predicate.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<QueryResultEntryV2026R0> Entries { get; set; }

        /// <summary>
        /// The marker for the start of the next page of results. When `null`, there
        /// are no further results available.
        /// </summary>
        [JsonPropertyName("next_marker")]
        public string NextMarker { get => _nextMarker; set { _nextMarker = value; _isNextMarkerSet = true; } }

        /// <summary>
        /// The limit that was used for this request. This will be the same as the limit query 
        /// parameter unless that value exceeded the maximum value allowed.
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        public QueryResultsV2026R0(IReadOnlyList<QueryResultEntryV2026R0> entries, int limit) {
            Entries = entries;
            Limit = limit;
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