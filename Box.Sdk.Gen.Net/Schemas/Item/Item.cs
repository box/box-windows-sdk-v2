using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ItemConverter))]
    public class Item {
        internal OneOf<FileFull, FolderMini, WebLink> _oneOf;
        
        public FileFull? FileFull => _oneOf._val0;
        
        public FolderMini? FolderMini => _oneOf._val1;
        
        public WebLink? WebLink => _oneOf._val2;
        
        public Item(FileFull value) {_oneOf = new OneOf<FileFull, FolderMini, WebLink>(value);}
        
        public Item(FolderMini value) {_oneOf = new OneOf<FileFull, FolderMini, WebLink>(value);}
        
        public Item(WebLink value) {_oneOf = new OneOf<FileFull, FolderMini, WebLink>(value);}
        
        public static implicit operator Item(FileFull value) => new Item(value);
        
        public static implicit operator Item(FolderMini value) => new Item(value);
        
        public static implicit operator Item(WebLink value) => new Item(value);
        
        class ItemConverter : JsonConverter<Item> {
            public override Item Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<FileFull>(document) ?? throw new Exception($"Could not deserialize {document} to FileFull");
                        case "folder":
                            return JsonSerializer.Deserialize<FolderMini>(document) ?? throw new Exception($"Could not deserialize {document} to FolderMini");
                        case "web_link":
                            return JsonSerializer.Deserialize<WebLink>(document) ?? throw new Exception($"Could not deserialize {document} to WebLink");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, Item? value, JsonSerializerOptions options) {
                if (value?.FileFull != null) {
                    JsonSerializer.Serialize(writer, value.FileFull, options);
                    return;
                }
                if (value?.FolderMini != null) {
                    JsonSerializer.Serialize(writer, value.FolderMini, options);
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