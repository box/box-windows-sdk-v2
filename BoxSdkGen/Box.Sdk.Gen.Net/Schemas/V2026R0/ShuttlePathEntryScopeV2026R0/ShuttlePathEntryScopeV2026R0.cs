using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ShuttlePathEntryScopeV2026R0Converter))]
    public class ShuttlePathEntryScopeV2026R0 {
        internal OneOf<ShuttlePathEntryAccountScopeV2026R0?, ShuttlePathEntrySharedSpaceScopeV2026R0?> _oneOf;
        
        public ShuttlePathEntryAccountScopeV2026R0? ShuttlePathEntryAccountScopeV2026R0 => _oneOf._val0;
        
        public ShuttlePathEntrySharedSpaceScopeV2026R0? ShuttlePathEntrySharedSpaceScopeV2026R0 => _oneOf._val1;
        
        public ShuttlePathEntryScopeV2026R0(ShuttlePathEntryAccountScopeV2026R0 value) {_oneOf = new OneOf<ShuttlePathEntryAccountScopeV2026R0?, ShuttlePathEntrySharedSpaceScopeV2026R0?>(value);}
        
        public ShuttlePathEntryScopeV2026R0(ShuttlePathEntrySharedSpaceScopeV2026R0 value) {_oneOf = new OneOf<ShuttlePathEntryAccountScopeV2026R0?, ShuttlePathEntrySharedSpaceScopeV2026R0?>(value);}
        
        public static implicit operator ShuttlePathEntryScopeV2026R0(ShuttlePathEntryAccountScopeV2026R0 value) => new ShuttlePathEntryScopeV2026R0(value);
        
        public static implicit operator ShuttlePathEntryScopeV2026R0(ShuttlePathEntrySharedSpaceScopeV2026R0 value) => new ShuttlePathEntryScopeV2026R0(value);
        
        class ShuttlePathEntryScopeV2026R0Converter : JsonConverter<ShuttlePathEntryScopeV2026R0> {
            public override ShuttlePathEntryScopeV2026R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "account":
                            return JsonSerializer.Deserialize<ShuttlePathEntryAccountScopeV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathEntryAccountScopeV2026R0");
                        case "shared_space":
                            return JsonSerializer.Deserialize<ShuttlePathEntrySharedSpaceScopeV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttlePathEntrySharedSpaceScopeV2026R0");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, ShuttlePathEntryScopeV2026R0? value, JsonSerializerOptions options) {
                if (value?.ShuttlePathEntryAccountScopeV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathEntryAccountScopeV2026R0, options);
                    return;
                }
                if (value?.ShuttlePathEntrySharedSpaceScopeV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttlePathEntrySharedSpaceScopeV2026R0, options);
                    return;
                }
            }

        }

    }
}