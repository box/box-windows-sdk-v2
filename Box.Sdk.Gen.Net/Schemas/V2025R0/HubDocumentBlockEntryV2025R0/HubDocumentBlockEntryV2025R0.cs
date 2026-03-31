using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(HubDocumentBlockEntryV2025R0Converter))]
    public class HubDocumentBlockEntryV2025R0 {
        internal OneOf<HubParagraphTextBlockV2025R0?, HubSectionTitleTextBlockV2025R0?, HubCalloutBoxTextBlockV2025R0?, HubItemListBlockV2025R0?, HubDividerBlockV2025R0?> _oneOf;
        
        public HubParagraphTextBlockV2025R0? HubParagraphTextBlockV2025R0 => _oneOf._val0;
        
        public HubSectionTitleTextBlockV2025R0? HubSectionTitleTextBlockV2025R0 => _oneOf._val1;
        
        public HubCalloutBoxTextBlockV2025R0? HubCalloutBoxTextBlockV2025R0 => _oneOf._val2;
        
        public HubItemListBlockV2025R0? HubItemListBlockV2025R0 => _oneOf._val3;
        
        public HubDividerBlockV2025R0? HubDividerBlockV2025R0 => _oneOf._val4;
        
        public HubDocumentBlockEntryV2025R0(HubParagraphTextBlockV2025R0 value) {_oneOf = new OneOf<HubParagraphTextBlockV2025R0?, HubSectionTitleTextBlockV2025R0?, HubCalloutBoxTextBlockV2025R0?, HubItemListBlockV2025R0?, HubDividerBlockV2025R0?>(value);}
        
        public HubDocumentBlockEntryV2025R0(HubSectionTitleTextBlockV2025R0 value) {_oneOf = new OneOf<HubParagraphTextBlockV2025R0?, HubSectionTitleTextBlockV2025R0?, HubCalloutBoxTextBlockV2025R0?, HubItemListBlockV2025R0?, HubDividerBlockV2025R0?>(value);}
        
        public HubDocumentBlockEntryV2025R0(HubCalloutBoxTextBlockV2025R0 value) {_oneOf = new OneOf<HubParagraphTextBlockV2025R0?, HubSectionTitleTextBlockV2025R0?, HubCalloutBoxTextBlockV2025R0?, HubItemListBlockV2025R0?, HubDividerBlockV2025R0?>(value);}
        
        public HubDocumentBlockEntryV2025R0(HubItemListBlockV2025R0 value) {_oneOf = new OneOf<HubParagraphTextBlockV2025R0?, HubSectionTitleTextBlockV2025R0?, HubCalloutBoxTextBlockV2025R0?, HubItemListBlockV2025R0?, HubDividerBlockV2025R0?>(value);}
        
        public HubDocumentBlockEntryV2025R0(HubDividerBlockV2025R0 value) {_oneOf = new OneOf<HubParagraphTextBlockV2025R0?, HubSectionTitleTextBlockV2025R0?, HubCalloutBoxTextBlockV2025R0?, HubItemListBlockV2025R0?, HubDividerBlockV2025R0?>(value);}
        
        public static implicit operator HubDocumentBlockEntryV2025R0(HubParagraphTextBlockV2025R0 value) => new HubDocumentBlockEntryV2025R0(value);
        
        public static implicit operator HubDocumentBlockEntryV2025R0(HubSectionTitleTextBlockV2025R0 value) => new HubDocumentBlockEntryV2025R0(value);
        
        public static implicit operator HubDocumentBlockEntryV2025R0(HubCalloutBoxTextBlockV2025R0 value) => new HubDocumentBlockEntryV2025R0(value);
        
        public static implicit operator HubDocumentBlockEntryV2025R0(HubItemListBlockV2025R0 value) => new HubDocumentBlockEntryV2025R0(value);
        
        public static implicit operator HubDocumentBlockEntryV2025R0(HubDividerBlockV2025R0 value) => new HubDocumentBlockEntryV2025R0(value);
        
        class HubDocumentBlockEntryV2025R0Converter : JsonConverter<HubDocumentBlockEntryV2025R0> {
            public override HubDocumentBlockEntryV2025R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "paragraph":
                            return JsonSerializer.Deserialize<HubParagraphTextBlockV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to HubParagraphTextBlockV2025R0");
                        case "section_title":
                            return JsonSerializer.Deserialize<HubSectionTitleTextBlockV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to HubSectionTitleTextBlockV2025R0");
                        case "callout_box":
                            return JsonSerializer.Deserialize<HubCalloutBoxTextBlockV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to HubCalloutBoxTextBlockV2025R0");
                        case "item_list":
                            return JsonSerializer.Deserialize<HubItemListBlockV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to HubItemListBlockV2025R0");
                        case "divider":
                            return JsonSerializer.Deserialize<HubDividerBlockV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to HubDividerBlockV2025R0");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, HubDocumentBlockEntryV2025R0? value, JsonSerializerOptions options) {
                if (value?.HubParagraphTextBlockV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.HubParagraphTextBlockV2025R0, options);
                    return;
                }
                if (value?.HubSectionTitleTextBlockV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.HubSectionTitleTextBlockV2025R0, options);
                    return;
                }
                if (value?.HubCalloutBoxTextBlockV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.HubCalloutBoxTextBlockV2025R0, options);
                    return;
                }
                if (value?.HubItemListBlockV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.HubItemListBlockV2025R0, options);
                    return;
                }
                if (value?.HubDividerBlockV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.HubDividerBlockV2025R0, options);
                    return;
                }
            }

        }

    }
}