using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(DocumentSplitPageSelectionSegmentV2026R0Converter))]
    public class DocumentSplitPageSelectionSegmentV2026R0 {
        internal OneOf<DocumentSplitPageSelectionSinglePageV2026R0, DocumentSplitPageSelectionPageRangeV2026R0> _oneOf;
        
        public DocumentSplitPageSelectionSinglePageV2026R0 DocumentSplitPageSelectionSinglePageV2026R0 => _oneOf._val0;
        
        public DocumentSplitPageSelectionPageRangeV2026R0 DocumentSplitPageSelectionPageRangeV2026R0 => _oneOf._val1;
        
        public DocumentSplitPageSelectionSegmentV2026R0(DocumentSplitPageSelectionSinglePageV2026R0 value) {_oneOf = new OneOf<DocumentSplitPageSelectionSinglePageV2026R0, DocumentSplitPageSelectionPageRangeV2026R0>(value);}
        
        public DocumentSplitPageSelectionSegmentV2026R0(DocumentSplitPageSelectionPageRangeV2026R0 value) {_oneOf = new OneOf<DocumentSplitPageSelectionSinglePageV2026R0, DocumentSplitPageSelectionPageRangeV2026R0>(value);}
        
        public static implicit operator DocumentSplitPageSelectionSegmentV2026R0(DocumentSplitPageSelectionSinglePageV2026R0 value) => new DocumentSplitPageSelectionSegmentV2026R0(value);
        
        public static implicit operator DocumentSplitPageSelectionSegmentV2026R0(DocumentSplitPageSelectionPageRangeV2026R0 value) => new DocumentSplitPageSelectionSegmentV2026R0(value);
        
        class DocumentSplitPageSelectionSegmentV2026R0Converter : JsonConverter<DocumentSplitPageSelectionSegmentV2026R0> {
            public override DocumentSplitPageSelectionSegmentV2026R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "single_page":
                                return JsonSerializer.Deserialize<DocumentSplitPageSelectionSinglePageV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to DocumentSplitPageSelectionSinglePageV2026R0");
                            case "page_range":
                                return JsonSerializer.Deserialize<DocumentSplitPageSelectionPageRangeV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to DocumentSplitPageSelectionPageRangeV2026R0");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, DocumentSplitPageSelectionSegmentV2026R0 value, JsonSerializerOptions options) {
                if (value?.DocumentSplitPageSelectionSinglePageV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.DocumentSplitPageSelectionSinglePageV2026R0, options);
                    return;
                }
                if (value?.DocumentSplitPageSelectionPageRangeV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.DocumentSplitPageSelectionPageRangeV2026R0, options);
                    return;
                }
            }

        }

    }
}