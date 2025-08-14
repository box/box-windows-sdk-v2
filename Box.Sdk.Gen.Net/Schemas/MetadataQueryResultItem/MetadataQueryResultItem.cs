using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(MetadataQueryResultItemConverter))]
    public class MetadataQueryResultItem {
        internal OneOf<FileFull, FolderFull> _oneOf;
        
        public FileFull? FileFull => _oneOf._val0;
        
        public FolderFull? FolderFull => _oneOf._val1;
        
        public MetadataQueryResultItem(FileFull value) {_oneOf = new OneOf<FileFull, FolderFull>(value);}
        
        public MetadataQueryResultItem(FolderFull value) {_oneOf = new OneOf<FileFull, FolderFull>(value);}
        
        public static implicit operator MetadataQueryResultItem(FileFull value) => new MetadataQueryResultItem(value);
        
        public static implicit operator MetadataQueryResultItem(FolderFull value) => new MetadataQueryResultItem(value);
        
        class MetadataQueryResultItemConverter : JsonConverter<MetadataQueryResultItem> {
            public override MetadataQueryResultItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<FileFull>(document) ?? throw new Exception($"Could not deserialize {document} to FileFull");
                        case "folder":
                            return JsonSerializer.Deserialize<FolderFull>(document) ?? throw new Exception($"Could not deserialize {document} to FolderFull");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, MetadataQueryResultItem? value, JsonSerializerOptions options) {
                if (value?.FileFull != null) {
                    JsonSerializer.Serialize(writer, value.FileFull, options);
                    return;
                }
                if (value?.FolderFull != null) {
                    JsonSerializer.Serialize(writer, value.FolderFull, options);
                    return;
                }
            }

        }

    }
}