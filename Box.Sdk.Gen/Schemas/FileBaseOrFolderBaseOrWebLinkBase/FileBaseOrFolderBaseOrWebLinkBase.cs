using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(FileBaseOrFolderBaseOrWebLinkBaseConverter))]
    public class FileBaseOrFolderBaseOrWebLinkBase : OneOf<FileBase, FolderBase, WebLinkBase> {
        public FileBase? FileBase => _val0;
        
        public FolderBase? FolderBase => _val1;
        
        public WebLinkBase? WebLinkBase => _val2;
        
        public FileBaseOrFolderBaseOrWebLinkBase(FileBase value) : base(value) {}
        
        public FileBaseOrFolderBaseOrWebLinkBase(FolderBase value) : base(value) {}
        
        public FileBaseOrFolderBaseOrWebLinkBase(WebLinkBase value) : base(value) {}
        
        public static implicit operator FileBaseOrFolderBaseOrWebLinkBase(FileBase value) => new FileBaseOrFolderBaseOrWebLinkBase(value);
        
        public static implicit operator FileBaseOrFolderBaseOrWebLinkBase(FolderBase value) => new FileBaseOrFolderBaseOrWebLinkBase(value);
        
        public static implicit operator FileBaseOrFolderBaseOrWebLinkBase(WebLinkBase value) => new FileBaseOrFolderBaseOrWebLinkBase(value);
        
        class FileBaseOrFolderBaseOrWebLinkBaseConverter : JsonConverter<FileBaseOrFolderBaseOrWebLinkBase> {
            public override FileBaseOrFolderBaseOrWebLinkBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<FileBase>(document) ?? throw new Exception($"Could not deserialize {document} to FileBase");
                        case "folder":
                            return JsonSerializer.Deserialize<FolderBase>(document) ?? throw new Exception($"Could not deserialize {document} to FolderBase");
                        case "web_link":
                            return JsonSerializer.Deserialize<WebLinkBase>(document) ?? throw new Exception($"Could not deserialize {document} to WebLinkBase");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, FileBaseOrFolderBaseOrWebLinkBase? value, JsonSerializerOptions options) {
                if (value?.FileBase != null) {
                    JsonSerializer.Serialize(writer, value.FileBase, options);
                    return;
                }
                if (value?.FolderBase != null) {
                    JsonSerializer.Serialize(writer, value.FolderBase, options);
                    return;
                }
                if (value?.WebLinkBase != null) {
                    JsonSerializer.Serialize(writer, value.WebLinkBase, options);
                    return;
                }
            }

        }

    }
}