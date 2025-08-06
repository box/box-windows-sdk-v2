using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SearchResults : ISerializable {
        /// <summary>
        /// One greater than the offset of the last entry in the search results.
        /// The total number of entries in the collection may be less than
        /// `total_count`.
        /// </summary>
        [JsonPropertyName("total_count")]
        public long? TotalCount { get; set; }

        /// <summary>
        /// The limit that was used for this search. This will be the same as the
        /// `limit` query parameter unless that value exceeded the maximum value
        /// allowed.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; set; }

        /// <summary>
        /// The 0-based offset of the first entry in this set. This will be the same
        /// as the `offset` query parameter used.
        /// </summary>
        [JsonPropertyName("offset")]
        public long? Offset { get; set; }

        /// <summary>
        /// Specifies the response as search result items without shared links.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SearchResultsTypeField>))]
        public StringEnum<SearchResultsTypeField> Type { get; set; }

        /// <summary>
        /// The search results for the query provided.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<FileFullOrFolderFullOrWebLink> Entries { get; set; }

        public SearchResults(SearchResultsTypeField type = SearchResultsTypeField.SearchResultsItems) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal SearchResults(StringEnum<SearchResultsTypeField> type) {
            Type = SearchResultsTypeField.SearchResultsItems;
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