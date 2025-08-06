using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0Converter))]
    public class FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0 {
        internal OneOf<FileReferenceV2025R0, FolderReferenceV2025R0, WeblinkReferenceV2025R0> _oneOf;
        
        public FileReferenceV2025R0 FileReferenceV2025R0 => _oneOf._val0;
        
        public FolderReferenceV2025R0 FolderReferenceV2025R0 => _oneOf._val1;
        
        public WeblinkReferenceV2025R0 WeblinkReferenceV2025R0 => _oneOf._val2;
        
        public FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(FileReferenceV2025R0 value) {_oneOf = new OneOf<FileReferenceV2025R0, FolderReferenceV2025R0, WeblinkReferenceV2025R0>(value);}
        
        public FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(FolderReferenceV2025R0 value) {_oneOf = new OneOf<FileReferenceV2025R0, FolderReferenceV2025R0, WeblinkReferenceV2025R0>(value);}
        
        public FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(WeblinkReferenceV2025R0 value) {_oneOf = new OneOf<FileReferenceV2025R0, FolderReferenceV2025R0, WeblinkReferenceV2025R0>(value);}
        
        public static implicit operator FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(FileReferenceV2025R0 value) => new FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(value);
        
        public static implicit operator FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(FolderReferenceV2025R0 value) => new FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(value);
        
        public static implicit operator FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(WeblinkReferenceV2025R0 value) => new FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0(value);
        
        class FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0Converter : JsonConverter<FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0> {
            public override FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "file":
                                return JsonSerializer.Deserialize<FileReferenceV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to FileReferenceV2025R0");
                            case "folder":
                                return JsonSerializer.Deserialize<FolderReferenceV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to FolderReferenceV2025R0");
                            case "weblink":
                                return JsonSerializer.Deserialize<WeblinkReferenceV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to WeblinkReferenceV2025R0");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0 value, JsonSerializerOptions options) {
                if (value?.FileReferenceV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.FileReferenceV2025R0, options);
                    return;
                }
                if (value?.FolderReferenceV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.FolderReferenceV2025R0, options);
                    return;
                }
                if (value?.WeblinkReferenceV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.WeblinkReferenceV2025R0, options);
                    return;
                }
            }

        }

    }
}