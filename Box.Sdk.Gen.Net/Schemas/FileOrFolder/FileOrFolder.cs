using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(FileOrFolderConverter))]
    public class FileOrFolder : OneOf<File, Folder> {
        public File? File => _val0;
        
        public Folder? Folder => _val1;
        
        public FileOrFolder(File value) : base(value) {}
        
        public FileOrFolder(Folder value) : base(value) {}
        
        public static implicit operator FileOrFolder(File value) => new FileOrFolder(value);
        
        public static implicit operator FileOrFolder(Folder value) => new FileOrFolder(value);
        
        class FileOrFolderConverter : JsonConverter<FileOrFolder> {
            public override FileOrFolder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<File>(document) ?? throw new Exception($"Could not deserialize {document} to File");
                        case "folder":
                            return JsonSerializer.Deserialize<Folder>(document) ?? throw new Exception($"Could not deserialize {document} to Folder");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, FileOrFolder? value, JsonSerializerOptions options) {
                if (value?.File != null) {
                    JsonSerializer.Serialize(writer, value.File, options);
                    return;
                }
                if (value?.Folder != null) {
                    JsonSerializer.Serialize(writer, value.Folder, options);
                    return;
                }
            }

        }

    }
}