using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(FileFullOrFolderMiniOrWebLinkConverter))]
    public class FileFullOrFolderMiniOrWebLink : OneOf<FileFull, FolderMini, WebLink> {
        public FileFull? FileFull => _val0;
        
        public FolderMini? FolderMini => _val1;
        
        public WebLink? WebLink => _val2;
        
        public FileFullOrFolderMiniOrWebLink(FileFull value) : base(value) {}
        
        public FileFullOrFolderMiniOrWebLink(FolderMini value) : base(value) {}
        
        public FileFullOrFolderMiniOrWebLink(WebLink value) : base(value) {}
        
        public static implicit operator FileFullOrFolderMiniOrWebLink(FileFull value) => new FileFullOrFolderMiniOrWebLink(value);
        
        public static implicit operator FileFullOrFolderMiniOrWebLink(FolderMini value) => new FileFullOrFolderMiniOrWebLink(value);
        
        public static implicit operator FileFullOrFolderMiniOrWebLink(WebLink value) => new FileFullOrFolderMiniOrWebLink(value);
        
        class FileFullOrFolderMiniOrWebLinkConverter : JsonConverter<FileFullOrFolderMiniOrWebLink> {
            public override FileFullOrFolderMiniOrWebLink Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
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

            public override void Write(Utf8JsonWriter writer, FileFullOrFolderMiniOrWebLink? value, JsonSerializerOptions options) {
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