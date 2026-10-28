using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ShuttlePathMappingLocationInputV2026R0Converter))]
    public class ShuttlePathMappingLocationInputV2026R0 {
        internal OneOf<ShuttlePathMappingAccountLocationInputV2026R0?, ShuttlePathMappingSharedSpaceLocationInputV2026R0?> _oneOf;
        
        public ShuttlePathMappingAccountLocationInputV2026R0? ShuttlePathMappingAccountLocationInputV2026R0 => _oneOf._val0;
        
        public ShuttlePathMappingSharedSpaceLocationInputV2026R0? ShuttlePathMappingSharedSpaceLocationInputV2026R0 => _oneOf._val1;
        
        public ShuttlePathMappingLocationInputV2026R0(ShuttlePathMappingAccountLocationInputV2026R0 value) {_oneOf = new OneOf<ShuttlePathMappingAccountLocationInputV2026R0?, ShuttlePathMappingSharedSpaceLocationInputV2026R0?>(value);}
        
        public ShuttlePathMappingLocationInputV2026R0(ShuttlePathMappingSharedSpaceLocationInputV2026R0 value) {_oneOf = new OneOf<ShuttlePathMappingAccountLocationInputV2026R0?, ShuttlePathMappingSharedSpaceLocationInputV2026R0?>(value);}
        
        public static implicit operator ShuttlePathMappingLocationInputV2026R0(ShuttlePathMappingAccountLocationInputV2026R0 value) => new ShuttlePathMappingLocationInputV2026R0(value);
        
        public static implicit operator ShuttlePathMappingLocationInputV2026R0(ShuttlePathMappingSharedSpaceLocationInputV2026R0 value) => new ShuttlePathMappingLocationInputV2026R0(value);
        
        class ShuttlePathMappingLocationInputV2026R0Converter : JsonConverter<ShuttlePathMappingLocationInputV2026R0> {
            public override ShuttlePathMappingLocationInputV2026R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "account":
                            return JsonSerializer.Deserialize<ShuttlePathMappingAccountLocationInputV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathMappingAccountLocationInputV2026R0");
                        case "shared_space":
                            return JsonSerializer.Deserialize<ShuttlePathMappingSharedSpaceLocationInputV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathMappingSharedSpaceLocationInputV2026R0");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, ShuttlePathMappingLocationInputV2026R0? value, JsonSerializerOptions options) {
                if (value?.ShuttlePathMappingAccountLocationInputV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathMappingAccountLocationInputV2026R0, options);
                    return;
                }
                if (value?.ShuttlePathMappingSharedSpaceLocationInputV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathMappingSharedSpaceLocationInputV2026R0, options);
                    return;
                }
            }

        }

    }
}