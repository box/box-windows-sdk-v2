using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ShieldListContentV2025R0Converter))]
    public class ShieldListContentV2025R0 : OneOf<ShieldListContentCountryV2025R0, ShieldListContentDomainV2025R0, ShieldListContentEmailV2025R0, ShieldListContentIpV2025R0, ShieldListContentIntegrationV2025R0> {
        public ShieldListContentCountryV2025R0? ShieldListContentCountryV2025R0 => _val0;
        
        public ShieldListContentDomainV2025R0? ShieldListContentDomainV2025R0 => _val1;
        
        public ShieldListContentEmailV2025R0? ShieldListContentEmailV2025R0 => _val2;
        
        public ShieldListContentIpV2025R0? ShieldListContentIpV2025R0 => _val3;
        
        public ShieldListContentIntegrationV2025R0? ShieldListContentIntegrationV2025R0 => _val4;
        
        public ShieldListContentV2025R0(ShieldListContentCountryV2025R0 value) : base(value) {}
        
        public ShieldListContentV2025R0(ShieldListContentDomainV2025R0 value) : base(value) {}
        
        public ShieldListContentV2025R0(ShieldListContentEmailV2025R0 value) : base(value) {}
        
        public ShieldListContentV2025R0(ShieldListContentIpV2025R0 value) : base(value) {}
        
        public ShieldListContentV2025R0(ShieldListContentIntegrationV2025R0 value) : base(value) {}
        
        public static implicit operator ShieldListContentV2025R0(ShieldListContentCountryV2025R0 value) => new ShieldListContentV2025R0(value);
        
        public static implicit operator ShieldListContentV2025R0(ShieldListContentDomainV2025R0 value) => new ShieldListContentV2025R0(value);
        
        public static implicit operator ShieldListContentV2025R0(ShieldListContentEmailV2025R0 value) => new ShieldListContentV2025R0(value);
        
        public static implicit operator ShieldListContentV2025R0(ShieldListContentIpV2025R0 value) => new ShieldListContentV2025R0(value);
        
        public static implicit operator ShieldListContentV2025R0(ShieldListContentIntegrationV2025R0 value) => new ShieldListContentV2025R0(value);
        
        class ShieldListContentV2025R0Converter : JsonConverter<ShieldListContentV2025R0> {
            public override ShieldListContentV2025R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "country":
                            return JsonSerializer.Deserialize<ShieldListContentCountryV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShieldListContentCountryV2025R0");
                        case "domain":
                            return JsonSerializer.Deserialize<ShieldListContentDomainV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShieldListContentDomainV2025R0");
                        case "email":
                            return JsonSerializer.Deserialize<ShieldListContentEmailV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShieldListContentEmailV2025R0");
                        case "ip":
                            return JsonSerializer.Deserialize<ShieldListContentIpV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShieldListContentIpV2025R0");
                        case "integration":
                            return JsonSerializer.Deserialize<ShieldListContentIntegrationV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShieldListContentIntegrationV2025R0");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, ShieldListContentV2025R0? value, JsonSerializerOptions options) {
                if (value?.ShieldListContentCountryV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShieldListContentCountryV2025R0, options);
                    return;
                }
                if (value?.ShieldListContentDomainV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShieldListContentDomainV2025R0, options);
                    return;
                }
                if (value?.ShieldListContentEmailV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShieldListContentEmailV2025R0, options);
                    return;
                }
                if (value?.ShieldListContentIpV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShieldListContentIpV2025R0, options);
                    return;
                }
                if (value?.ShieldListContentIntegrationV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShieldListContentIntegrationV2025R0, options);
                    return;
                }
            }

        }

    }
}