using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ShuttlePathMappingTargetLocationInputV2026R0Converter))]
    public class ShuttlePathMappingTargetLocationInputV2026R0 {
        internal OneOf<ShuttlePathMappingAccountLocationTargetInputV2026R0, ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0> _oneOf;
        
        public ShuttlePathMappingAccountLocationTargetInputV2026R0 ShuttlePathMappingAccountLocationTargetInputV2026R0 => _oneOf._val0;
        
        public ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0 ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0 => _oneOf._val1;
        
        public ShuttlePathMappingTargetLocationInputV2026R0(ShuttlePathMappingAccountLocationTargetInputV2026R0 value) {_oneOf = new OneOf<ShuttlePathMappingAccountLocationTargetInputV2026R0, ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0>(value);}
        
        public ShuttlePathMappingTargetLocationInputV2026R0(ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0 value) {_oneOf = new OneOf<ShuttlePathMappingAccountLocationTargetInputV2026R0, ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0>(value);}
        
        public static implicit operator ShuttlePathMappingTargetLocationInputV2026R0(ShuttlePathMappingAccountLocationTargetInputV2026R0 value) => new ShuttlePathMappingTargetLocationInputV2026R0(value);
        
        public static implicit operator ShuttlePathMappingTargetLocationInputV2026R0(ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0 value) => new ShuttlePathMappingTargetLocationInputV2026R0(value);
        
        class ShuttlePathMappingTargetLocationInputV2026R0Converter : JsonConverter<ShuttlePathMappingTargetLocationInputV2026R0> {
            public override ShuttlePathMappingTargetLocationInputV2026R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "account":
                                return JsonSerializer.Deserialize<ShuttlePathMappingAccountLocationTargetInputV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathMappingAccountLocationTargetInputV2026R0");
                            case "shared_space":
                                return JsonSerializer.Deserialize<ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, ShuttlePathMappingTargetLocationInputV2026R0 value, JsonSerializerOptions options) {
                if (value?.ShuttlePathMappingAccountLocationTargetInputV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathMappingAccountLocationTargetInputV2026R0, options);
                    return;
                }
                if (value?.ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathMappingSharedSpaceLocationTargetInputV2026R0, options);
                    return;
                }
            }

        }

    }
}