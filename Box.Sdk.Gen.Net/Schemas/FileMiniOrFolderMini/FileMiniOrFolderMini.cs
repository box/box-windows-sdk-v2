using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(FileMiniOrFolderMiniConverter))]
    public class FileMiniOrFolderMini : OneOf<FileMini, FolderMini> {
        public FileMini? FileMini => _val0;
        
        public FolderMini? FolderMini => _val1;
        
        public FileMiniOrFolderMini(FileMini value) : base(value) {}
        
        public FileMiniOrFolderMini(FolderMini value) : base(value) {}
        
        public static implicit operator FileMiniOrFolderMini(FileMini value) => new FileMiniOrFolderMini(value);
        
        public static implicit operator FileMiniOrFolderMini(FolderMini value) => new FileMiniOrFolderMini(value);
        
        class FileMiniOrFolderMiniConverter : JsonConverter<FileMiniOrFolderMini> {
            public override FileMiniOrFolderMini Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<FileMini>(document) ?? throw new Exception($"Could not deserialize {document} to FileMini");
                        case "folder":
                            return JsonSerializer.Deserialize<FolderMini>(document) ?? throw new Exception($"Could not deserialize {document} to FolderMini");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, FileMiniOrFolderMini? value, JsonSerializerOptions options) {
                if (value?.FileMini != null) {
                    JsonSerializer.Serialize(writer, value.FileMini, options);
                    return;
                }
                if (value?.FolderMini != null) {
                    JsonSerializer.Serialize(writer, value.FolderMini, options);
                    return;
                }
            }

        }

    }
}