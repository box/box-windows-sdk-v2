using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ResourceConverter))]
    public class Resource {
        internal OneOf<FolderMini, FileMini> _oneOf;
        
        public FolderMini? FolderMini => _oneOf._val0;
        
        public FileMini? FileMini => _oneOf._val1;
        
        public Resource(FolderMini value) {_oneOf = new OneOf<FolderMini, FileMini>(value);}
        
        public Resource(FileMini value) {_oneOf = new OneOf<FolderMini, FileMini>(value);}
        
        public static implicit operator Resource(FolderMini value) => new Resource(value);
        
        public static implicit operator Resource(FileMini value) => new Resource(value);
        
        class ResourceConverter : JsonConverter<Resource> {
            public override Resource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "folder":
                            return JsonSerializer.Deserialize<FolderMini>(document) ?? throw new Exception($"Could not deserialize {document} to FolderMini");
                        case "file":
                            return JsonSerializer.Deserialize<FileMini>(document) ?? throw new Exception($"Could not deserialize {document} to FileMini");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, Resource? value, JsonSerializerOptions options) {
                if (value?.FolderMini != null) {
                    JsonSerializer.Serialize(writer, value.FolderMini, options);
                    return;
                }
                if (value?.FileMini != null) {
                    JsonSerializer.Serialize(writer, value.FileMini, options);
                    return;
                }
            }

        }

    }
}