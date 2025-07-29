using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(FileFullOrFolderFullConverter))]
    public class FileFullOrFolderFull : OneOf<FileFull, FolderFull> {
        public FileFull? FileFull => _val0;
        
        public FolderFull? FolderFull => _val1;
        
        public FileFullOrFolderFull(FileFull value) : base(value) {}
        
        public FileFullOrFolderFull(FolderFull value) : base(value) {}
        
        public static implicit operator FileFullOrFolderFull(FileFull value) => new FileFullOrFolderFull(value);
        
        public static implicit operator FileFullOrFolderFull(FolderFull value) => new FileFullOrFolderFull(value);
        
        class FileFullOrFolderFullConverter : JsonConverter<FileFullOrFolderFull> {
            public override FileFullOrFolderFull Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
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

            public override void Write(Utf8JsonWriter writer, FileFullOrFolderFull? value, JsonSerializerOptions options) {
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