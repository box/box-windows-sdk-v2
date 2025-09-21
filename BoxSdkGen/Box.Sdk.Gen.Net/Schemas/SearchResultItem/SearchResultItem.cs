using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(SearchResultItemConverter))]
    public class SearchResultItem {
        internal OneOf<FileFull, FolderFull, WebLink> _oneOf;
        
        public FileFull? FileFull => _oneOf._val0;
        
        public FolderFull? FolderFull => _oneOf._val1;
        
        public WebLink? WebLink => _oneOf._val2;
        
        public SearchResultItem(FileFull value) {_oneOf = new OneOf<FileFull, FolderFull, WebLink>(value);}
        
        public SearchResultItem(FolderFull value) {_oneOf = new OneOf<FileFull, FolderFull, WebLink>(value);}
        
        public SearchResultItem(WebLink value) {_oneOf = new OneOf<FileFull, FolderFull, WebLink>(value);}
        
        public static implicit operator SearchResultItem(FileFull value) => new SearchResultItem(value);
        
        public static implicit operator SearchResultItem(FolderFull value) => new SearchResultItem(value);
        
        public static implicit operator SearchResultItem(WebLink value) => new SearchResultItem(value);
        
        class SearchResultItemConverter : JsonConverter<SearchResultItem> {
            public override SearchResultItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<FileFull>(document) ?? throw new Exception($"Could not deserialize {document} to FileFull");
                        case "folder":
                            return JsonSerializer.Deserialize<FolderFull>(document) ?? throw new Exception($"Could not deserialize {document} to FolderFull");
                        case "web_link":
                            return JsonSerializer.Deserialize<WebLink>(document) ?? throw new Exception($"Could not deserialize {document} to WebLink");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, SearchResultItem? value, JsonSerializerOptions options) {
                if (value?.FileFull != null) {
                    JsonSerializer.Serialize(writer, value.FileFull, options);
                    return;
                }
                if (value?.FolderFull != null) {
                    JsonSerializer.Serialize(writer, value.FolderFull, options);
                    return;
                }
                if (value?.WebLink != null) {
                    JsonSerializer.Serialize(writer, value.WebLink, options);
                    return;
                }
            }

        }

    }
}