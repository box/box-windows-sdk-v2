using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ShuttlePathMappingLocationV2026R0Converter))]
    public class ShuttlePathMappingLocationV2026R0 {
        internal OneOf<ShuttlePathMappingAccountLocationV2026R0, ShuttlePathMappingSharedSpaceLocationV2026R0> _oneOf;
        
        public ShuttlePathMappingAccountLocationV2026R0 ShuttlePathMappingAccountLocationV2026R0 => _oneOf._val0;
        
        public ShuttlePathMappingSharedSpaceLocationV2026R0 ShuttlePathMappingSharedSpaceLocationV2026R0 => _oneOf._val1;
        
        public ShuttlePathMappingLocationV2026R0(ShuttlePathMappingAccountLocationV2026R0 value) {_oneOf = new OneOf<ShuttlePathMappingAccountLocationV2026R0, ShuttlePathMappingSharedSpaceLocationV2026R0>(value);}
        
        public ShuttlePathMappingLocationV2026R0(ShuttlePathMappingSharedSpaceLocationV2026R0 value) {_oneOf = new OneOf<ShuttlePathMappingAccountLocationV2026R0, ShuttlePathMappingSharedSpaceLocationV2026R0>(value);}
        
        public static implicit operator ShuttlePathMappingLocationV2026R0(ShuttlePathMappingAccountLocationV2026R0 value) => new ShuttlePathMappingLocationV2026R0(value);
        
        public static implicit operator ShuttlePathMappingLocationV2026R0(ShuttlePathMappingSharedSpaceLocationV2026R0 value) => new ShuttlePathMappingLocationV2026R0(value);
        
        class ShuttlePathMappingLocationV2026R0Converter : JsonConverter<ShuttlePathMappingLocationV2026R0> {
            public override ShuttlePathMappingLocationV2026R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "account":
                                return JsonSerializer.Deserialize<ShuttlePathMappingAccountLocationV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathMappingAccountLocationV2026R0");
                            case "shared_space":
                                return JsonSerializer.Deserialize<ShuttlePathMappingSharedSpaceLocationV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathMappingSharedSpaceLocationV2026R0");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, ShuttlePathMappingLocationV2026R0 value, JsonSerializerOptions options) {
                if (value?.ShuttlePathMappingAccountLocationV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathMappingAccountLocationV2026R0, options);
                    return;
                }
                if (value?.ShuttlePathMappingSharedSpaceLocationV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathMappingSharedSpaceLocationV2026R0, options);
                    return;
                }
            }

        }

    }
}