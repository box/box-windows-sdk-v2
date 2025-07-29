using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(SearchResultsOrSearchResultsWithSharedLinksConverter))]
    public class SearchResultsOrSearchResultsWithSharedLinks : OneOf<SearchResults, SearchResultsWithSharedLinks> {
        public SearchResults? SearchResults => _val0;
        
        public SearchResultsWithSharedLinks? SearchResultsWithSharedLinks => _val1;
        
        public SearchResultsOrSearchResultsWithSharedLinks(SearchResults value) : base(value) {}
        
        public SearchResultsOrSearchResultsWithSharedLinks(SearchResultsWithSharedLinks value) : base(value) {}
        
        public static implicit operator SearchResultsOrSearchResultsWithSharedLinks(SearchResults value) => new SearchResultsOrSearchResultsWithSharedLinks(value);
        
        public static implicit operator SearchResultsOrSearchResultsWithSharedLinks(SearchResultsWithSharedLinks value) => new SearchResultsOrSearchResultsWithSharedLinks(value);
        
        class SearchResultsOrSearchResultsWithSharedLinksConverter : JsonConverter<SearchResultsOrSearchResultsWithSharedLinks> {
            public override SearchResultsOrSearchResultsWithSharedLinks Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "search_results_items":
                            return JsonSerializer.Deserialize<SearchResults>(document) ?? throw new Exception($"Could not deserialize {document} to SearchResults");
                        case "search_results_with_shared_links":
                            return JsonSerializer.Deserialize<SearchResultsWithSharedLinks>(document) ?? throw new Exception($"Could not deserialize {document} to SearchResultsWithSharedLinks");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, SearchResultsOrSearchResultsWithSharedLinks? value, JsonSerializerOptions options) {
                if (value?.SearchResults != null) {
                    JsonSerializer.Serialize(writer, value.SearchResults, options);
                    return;
                }
                if (value?.SearchResultsWithSharedLinks != null) {
                    JsonSerializer.Serialize(writer, value.SearchResultsWithSharedLinks, options);
                    return;
                }
            }

        }

    }
}