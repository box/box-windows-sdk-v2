using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class LegalHoldPolicies : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnext_markerSet")]
        protected bool _isNextMarkerSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isprev_markerSet")]
        protected bool _isPrevMarkerSet { get; set; }

        protected string _nextMarker { get; set; }

        protected string _prevMarker { get; set; }

        /// <summary>
        /// The limit that was used for these entries. This will be the same as the
        /// `limit` query parameter unless that value exceeded the maximum value
        /// allowed. The maximum value varies by API.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; set; }

        /// <summary>
        /// The marker for the start of the next page of results.
        /// </summary>
        [JsonPropertyName("next_marker")]
        public string NextMarker { get => _nextMarker; set { _nextMarker = value; _isNextMarkerSet = true; } }

        /// <summary>
        /// The marker for the start of the previous page of results.
        /// </summary>
        [JsonPropertyName("prev_marker")]
        public string PrevMarker { get => _prevMarker; set { _prevMarker = value; _isPrevMarkerSet = true; } }

        /// <summary>
        /// A list of legal hold policies.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<LegalHoldPolicy> Entries { get; set; }

        public LegalHoldPolicies() {
            
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