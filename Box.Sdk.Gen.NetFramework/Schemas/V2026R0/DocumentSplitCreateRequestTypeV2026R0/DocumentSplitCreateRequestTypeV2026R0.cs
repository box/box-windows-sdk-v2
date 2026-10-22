using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(DocumentSplitCreateRequestTypeV2026R0Converter))]
    public class DocumentSplitCreateRequestTypeV2026R0 {
        internal OneOf<DocumentSplitCreateRequestManualV2026R0, DocumentSplitCreateRequestPageSelectionV2026R0, DocumentSplitCreateRequestSmartV2026R0> _oneOf;
        
        public DocumentSplitCreateRequestManualV2026R0 DocumentSplitCreateRequestManualV2026R0 => _oneOf._val0;
        
        public DocumentSplitCreateRequestPageSelectionV2026R0 DocumentSplitCreateRequestPageSelectionV2026R0 => _oneOf._val1;
        
        public DocumentSplitCreateRequestSmartV2026R0 DocumentSplitCreateRequestSmartV2026R0 => _oneOf._val2;
        
        public DocumentSplitCreateRequestTypeV2026R0(DocumentSplitCreateRequestManualV2026R0 value) {_oneOf = new OneOf<DocumentSplitCreateRequestManualV2026R0, DocumentSplitCreateRequestPageSelectionV2026R0, DocumentSplitCreateRequestSmartV2026R0>(value);}
        
        public DocumentSplitCreateRequestTypeV2026R0(DocumentSplitCreateRequestPageSelectionV2026R0 value) {_oneOf = new OneOf<DocumentSplitCreateRequestManualV2026R0, DocumentSplitCreateRequestPageSelectionV2026R0, DocumentSplitCreateRequestSmartV2026R0>(value);}
        
        public DocumentSplitCreateRequestTypeV2026R0(DocumentSplitCreateRequestSmartV2026R0 value) {_oneOf = new OneOf<DocumentSplitCreateRequestManualV2026R0, DocumentSplitCreateRequestPageSelectionV2026R0, DocumentSplitCreateRequestSmartV2026R0>(value);}
        
        public static implicit operator DocumentSplitCreateRequestTypeV2026R0(DocumentSplitCreateRequestManualV2026R0 value) => new DocumentSplitCreateRequestTypeV2026R0(value);
        
        public static implicit operator DocumentSplitCreateRequestTypeV2026R0(DocumentSplitCreateRequestPageSelectionV2026R0 value) => new DocumentSplitCreateRequestTypeV2026R0(value);
        
        public static implicit operator DocumentSplitCreateRequestTypeV2026R0(DocumentSplitCreateRequestSmartV2026R0 value) => new DocumentSplitCreateRequestTypeV2026R0(value);
        
        class DocumentSplitCreateRequestTypeV2026R0Converter : JsonConverter<DocumentSplitCreateRequestTypeV2026R0> {
            public override DocumentSplitCreateRequestTypeV2026R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    try {
                        var result = JsonSerializer.Deserialize<DocumentSplitCreateRequestManualV2026R0>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<DocumentSplitCreateRequestPageSelectionV2026R0>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<DocumentSplitCreateRequestSmartV2026R0>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, DocumentSplitCreateRequestTypeV2026R0 value, JsonSerializerOptions options) {
                if (value?.DocumentSplitCreateRequestManualV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.DocumentSplitCreateRequestManualV2026R0, options);
                    return;
                }
                if (value?.DocumentSplitCreateRequestPageSelectionV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.DocumentSplitCreateRequestPageSelectionV2026R0, options);
                    return;
                }
                if (value?.DocumentSplitCreateRequestSmartV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.DocumentSplitCreateRequestSmartV2026R0, options);
                    return;
                }
            }

        }

    }
}