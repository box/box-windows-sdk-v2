using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationsV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnext_markerSet")]
        protected bool _isNextMarkerSet { get; set; }

        protected string? _nextMarker { get; set; }

        /// <summary>
        /// A list of hub collaborations.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<HubCollaborationV2025R0>? Entries { get; init; }

        /// <summary>
        /// The limit that was used for these entries. This will be the same as the
        /// `limit` query parameter unless that value exceeded the maximum value
        /// allowed. The maximum value varies by API.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; init; }

        /// <summary>
        /// The marker for the start of the next page of results.
        /// </summary>
        [JsonPropertyName("next_marker")]
        public string? NextMarker { get => _nextMarker; init { _nextMarker = value; _isNextMarkerSet = true; } }

        public HubCollaborationsV2025R0() {
            
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}