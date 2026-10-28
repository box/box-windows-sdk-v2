using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(ShuttleJobScheduleTriggerV2026R0Converter))]
    public class ShuttleJobScheduleTriggerV2026R0 {
        internal OneOf<ShuttleJobScheduleTriggerByTimeV2026R0?, ShuttleJobScheduleTriggerByJobV2026R0?> _oneOf;
        
        public ShuttleJobScheduleTriggerByTimeV2026R0? ShuttleJobScheduleTriggerByTimeV2026R0 => _oneOf._val0;
        
        public ShuttleJobScheduleTriggerByJobV2026R0? ShuttleJobScheduleTriggerByJobV2026R0 => _oneOf._val1;
        
        public ShuttleJobScheduleTriggerV2026R0(ShuttleJobScheduleTriggerByTimeV2026R0 value) {_oneOf = new OneOf<ShuttleJobScheduleTriggerByTimeV2026R0?, ShuttleJobScheduleTriggerByJobV2026R0?>(value);}
        
        public ShuttleJobScheduleTriggerV2026R0(ShuttleJobScheduleTriggerByJobV2026R0 value) {_oneOf = new OneOf<ShuttleJobScheduleTriggerByTimeV2026R0?, ShuttleJobScheduleTriggerByJobV2026R0?>(value);}
        
        public static implicit operator ShuttleJobScheduleTriggerV2026R0(ShuttleJobScheduleTriggerByTimeV2026R0 value) => new ShuttleJobScheduleTriggerV2026R0(value);
        
        public static implicit operator ShuttleJobScheduleTriggerV2026R0(ShuttleJobScheduleTriggerByJobV2026R0 value) => new ShuttleJobScheduleTriggerV2026R0(value);
        
        class ShuttleJobScheduleTriggerV2026R0Converter : JsonConverter<ShuttleJobScheduleTriggerV2026R0> {
            public override ShuttleJobScheduleTriggerV2026R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "by_time":
                            return JsonSerializer.Deserialize<ShuttleJobScheduleTriggerByTimeV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttleJobScheduleTriggerByTimeV2026R0");
                        case "by_job":
                            return JsonSerializer.Deserialize<ShuttleJobScheduleTriggerByJobV2026R0>(document) ?? throw new Exception($"Could not deserialize {document} to ShuttleJobScheduleTriggerByJobV2026R0");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, ShuttleJobScheduleTriggerV2026R0? value, JsonSerializerOptions options) {
                if (value?.ShuttleJobScheduleTriggerByTimeV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttleJobScheduleTriggerByTimeV2026R0, options);
                    return;
                }
                if (value?.ShuttleJobScheduleTriggerByJobV2026R0 != null) {
                    JsonSerializer.Serialize(writer, value.ShuttleJobScheduleTriggerByJobV2026R0, options);
                    return;
                }
            }

        }

    }
}