using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataQueryResults : ISerializable {
        /// <summary>
        /// The mini representation of the files and folders that match the search
        /// terms.
        /// 
        /// By default, this endpoint returns only the most basic info about the
        /// items. To get additional fields for each item, including any of the
        /// metadata, use the `fields` attribute in the query.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<FileFullOrFolderFull>? Entries { get; init; }

        /// <summary>
        /// The limit that was used for this search. This will be the same as the
        /// `limit` query parameter unless that value exceeded the maximum value
        /// allowed.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; init; }

        /// <summary>
        /// The marker for the start of the next page of results.
        /// </summary>
        [JsonPropertyName("next_marker")]
        public string? NextMarker { get; init; }

        public MetadataQueryResults() {
            
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