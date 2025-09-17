using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(CollaborationItemConverter))]
    public class CollaborationItem {
        internal OneOf<File, Folder, WebLink> _oneOf;
        
        public File? File => _oneOf._val0;
        
        public Folder? Folder => _oneOf._val1;
        
        public WebLink? WebLink => _oneOf._val2;
        
        public CollaborationItem(File value) {_oneOf = new OneOf<File, Folder, WebLink>(value);}
        
        public CollaborationItem(Folder value) {_oneOf = new OneOf<File, Folder, WebLink>(value);}
        
        public CollaborationItem(WebLink value) {_oneOf = new OneOf<File, Folder, WebLink>(value);}
        
        public static implicit operator CollaborationItem(File value) => new CollaborationItem(value);
        
        public static implicit operator CollaborationItem(Folder value) => new CollaborationItem(value);
        
        public static implicit operator CollaborationItem(WebLink value) => new CollaborationItem(value);
        
        class CollaborationItemConverter : JsonConverter<CollaborationItem> {
            public override CollaborationItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<File>(document) ?? throw new Exception($"Could not deserialize {document} to File");
                        case "folder":
                            return JsonSerializer.Deserialize<Folder>(document) ?? throw new Exception($"Could not deserialize {document} to Folder");
                        case "web_link":
                            return JsonSerializer.Deserialize<WebLink>(document) ?? throw new Exception($"Could not deserialize {document} to WebLink");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, CollaborationItem? value, JsonSerializerOptions options) {
                if (value?.File != null) {
                    JsonSerializer.Serialize(writer, value.File, options);
                    return;
                }
                if (value?.Folder != null) {
                    JsonSerializer.Serialize(writer, value.Folder, options);
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