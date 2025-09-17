using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ShieldListContentRequestV2025R0Converter))]
    public class ShieldListContentRequestV2025R0 {
        internal OneOf<ShieldListContentCountryV2025R0, ShieldListContentDomainV2025R0, ShieldListContentEmailV2025R0, ShieldListContentIpV2025R0> _oneOf;
        
        public ShieldListContentCountryV2025R0? ShieldListContentCountryV2025R0 => _oneOf._val0;
        
        public ShieldListContentDomainV2025R0? ShieldListContentDomainV2025R0 => _oneOf._val1;
        
        public ShieldListContentEmailV2025R0? ShieldListContentEmailV2025R0 => _oneOf._val2;
        
        public ShieldListContentIpV2025R0? ShieldListContentIpV2025R0 => _oneOf._val3;
        
        public ShieldListContentRequestV2025R0(ShieldListContentCountryV2025R0 value) {_oneOf = new OneOf<ShieldListContentCountryV2025R0, ShieldListContentDomainV2025R0, ShieldListContentEmailV2025R0, ShieldListContentIpV2025R0>(value);}
        
        public ShieldListContentRequestV2025R0(ShieldListContentDomainV2025R0 value) {_oneOf = new OneOf<ShieldListContentCountryV2025R0, ShieldListContentDomainV2025R0, ShieldListContentEmailV2025R0, ShieldListContentIpV2025R0>(value);}
        
        public ShieldListContentRequestV2025R0(ShieldListContentEmailV2025R0 value) {_oneOf = new OneOf<ShieldListContentCountryV2025R0, ShieldListContentDomainV2025R0, ShieldListContentEmailV2025R0, ShieldListContentIpV2025R0>(value);}
        
        public ShieldListContentRequestV2025R0(ShieldListContentIpV2025R0 value) {_oneOf = new OneOf<ShieldListContentCountryV2025R0, ShieldListContentDomainV2025R0, ShieldListContentEmailV2025R0, ShieldListContentIpV2025R0>(value);}
        
        public static implicit operator ShieldListContentRequestV2025R0(ShieldListContentCountryV2025R0 value) => new ShieldListContentRequestV2025R0(value);
        
        public static implicit operator ShieldListContentRequestV2025R0(ShieldListContentDomainV2025R0 value) => new ShieldListContentRequestV2025R0(value);
        
        public static implicit operator ShieldListContentRequestV2025R0(ShieldListContentEmailV2025R0 value) => new ShieldListContentRequestV2025R0(value);
        
        public static implicit operator ShieldListContentRequestV2025R0(ShieldListContentIpV2025R0 value) => new ShieldListContentRequestV2025R0(value);
        
        class ShieldListContentRequestV2025R0Converter : JsonConverter<ShieldListContentRequestV2025R0> {
            public override ShieldListContentRequestV2025R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
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
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, ShieldListContentRequestV2025R0? value, JsonSerializerOptions options) {
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
            }

        }

    }
}