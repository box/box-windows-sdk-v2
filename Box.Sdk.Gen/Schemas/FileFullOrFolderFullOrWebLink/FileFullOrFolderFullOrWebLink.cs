using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(FileFullOrFolderFullOrWebLinkConverter))]
    public class FileFullOrFolderFullOrWebLink : OneOf<FileFull, FolderFull, WebLink> {
        public FileFull? FileFull => _val0;
        
        public FolderFull? FolderFull => _val1;
        
        public WebLink? WebLink => _val2;
        
        public FileFullOrFolderFullOrWebLink(FileFull value) : base(value) {}
        
        public FileFullOrFolderFullOrWebLink(FolderFull value) : base(value) {}
        
        public FileFullOrFolderFullOrWebLink(WebLink value) : base(value) {}
        
        public static implicit operator FileFullOrFolderFullOrWebLink(FileFull value) => new FileFullOrFolderFullOrWebLink(value);
        
        public static implicit operator FileFullOrFolderFullOrWebLink(FolderFull value) => new FileFullOrFolderFullOrWebLink(value);
        
        public static implicit operator FileFullOrFolderFullOrWebLink(WebLink value) => new FileFullOrFolderFullOrWebLink(value);
        
        class FileFullOrFolderFullOrWebLinkConverter : JsonConverter<FileFullOrFolderFullOrWebLink> {
            public override FileFullOrFolderFullOrWebLink Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
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

            public override void Write(Utf8JsonWriter writer, FileFullOrFolderFullOrWebLink? value, JsonSerializerOptions options) {
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