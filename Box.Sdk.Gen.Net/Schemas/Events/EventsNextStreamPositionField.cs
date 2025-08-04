using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(EventsNextStreamPositionFieldConverter))]
    public class EventsNextStreamPositionField {
        internal OneOf<string, long> _oneOf;
        
        public string? StringVal => _oneOf._val0;
        
        public long? LongVal => _oneOf._val1;
        
        public EventsNextStreamPositionField(string value) {_oneOf = new OneOf<string, long>(value);}
        
        public EventsNextStreamPositionField(long value) {_oneOf = new OneOf<string, long>(value);}
        
        public static implicit operator EventsNextStreamPositionField(string value) => new EventsNextStreamPositionField(value);
        
        public static implicit operator EventsNextStreamPositionField(long value) => new EventsNextStreamPositionField(value);
        
        class EventsNextStreamPositionFieldConverter : JsonConverter<EventsNextStreamPositionField> {
            public override EventsNextStreamPositionField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                try {
                    var result = JsonSerializer.Deserialize<string>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return result;
                    }
                } catch {
                    
                }
                try {
                    var result = JsonSerializer.Deserialize<long>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    return result;
                } catch {
                    
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, EventsNextStreamPositionField? value, JsonSerializerOptions options) {
                if (value?.StringVal != null) {
                    JsonSerializer.Serialize(writer, value.StringVal, options);
                    return;
                }
                if (value?.LongVal != null) {
                    JsonSerializer.Serialize(writer, value.LongVal, options);
                    return;
                }
            }

        }

    }
}