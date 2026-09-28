using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(CollaborationItemConverter))]
    public class CollaborationItem {
        internal OneOf<FileMini?, FolderMini?, WebLinkMini?> _oneOf;
        
        public FileMini? FileMini => _oneOf._val0;
        
        public FolderMini? FolderMini => _oneOf._val1;
        
        public WebLinkMini? WebLinkMini => _oneOf._val2;
        
        public CollaborationItem(FileMini value) {_oneOf = new OneOf<FileMini?, FolderMini?, WebLinkMini?>(value);}
        
        public CollaborationItem(FolderMini value) {_oneOf = new OneOf<FileMini?, FolderMini?, WebLinkMini?>(value);}
        
        public CollaborationItem(WebLinkMini value) {_oneOf = new OneOf<FileMini?, FolderMini?, WebLinkMini?>(value);}
        
        public static implicit operator CollaborationItem(FileMini value) => new CollaborationItem(value);
        
        public static implicit operator CollaborationItem(FolderMini value) => new CollaborationItem(value);
        
        public static implicit operator CollaborationItem(WebLinkMini value) => new CollaborationItem(value);
        
        class CollaborationItemConverter : JsonConverter<CollaborationItem> {
            public override CollaborationItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<FileMini>(document) ?? throw new Exception($"Could not deserialize {document} to FileMini");
                        case "folder":
                            return JsonSerializer.Deserialize<FolderMini>(document) ?? throw new Exception($"Could not deserialize {document} to FolderMini");
                        case "web_link":
                            return JsonSerializer.Deserialize<WebLinkMini>(document) ?? throw new Exception($"Could not deserialize {document} to WebLinkMini");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, CollaborationItem? value, JsonSerializerOptions options) {
                if (value?.FileMini != null) {
                    JsonSerializer.Serialize(writer, value.FileMini, options);
                    return;
                }
                if (value?.FolderMini != null) {
                    JsonSerializer.Serialize(writer, value.FolderMini, options);
                    return;
                }
                if (value?.WebLinkMini != null) {
                    JsonSerializer.Serialize(writer, value.WebLinkMini, options);
                    return;
                }
            }

        }

    }
}