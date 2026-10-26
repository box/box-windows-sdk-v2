using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(SharedLinkOtpWritableConverter))]
    public class SharedLinkOtpWritable {
        internal OneOf<SharedLinkOtpWritableNone, SharedLinkOtpWritableOpen, SharedLinkOtpWritableAllowlist> _oneOf;
        
        public SharedLinkOtpWritableNone SharedLinkOtpWritableNone => _oneOf._val0;
        
        public SharedLinkOtpWritableOpen SharedLinkOtpWritableOpen => _oneOf._val1;
        
        public SharedLinkOtpWritableAllowlist SharedLinkOtpWritableAllowlist => _oneOf._val2;
        
        public SharedLinkOtpWritable(SharedLinkOtpWritableNone value) {_oneOf = new OneOf<SharedLinkOtpWritableNone, SharedLinkOtpWritableOpen, SharedLinkOtpWritableAllowlist>(value);}
        
        public SharedLinkOtpWritable(SharedLinkOtpWritableOpen value) {_oneOf = new OneOf<SharedLinkOtpWritableNone, SharedLinkOtpWritableOpen, SharedLinkOtpWritableAllowlist>(value);}
        
        public SharedLinkOtpWritable(SharedLinkOtpWritableAllowlist value) {_oneOf = new OneOf<SharedLinkOtpWritableNone, SharedLinkOtpWritableOpen, SharedLinkOtpWritableAllowlist>(value);}
        
        public static implicit operator SharedLinkOtpWritable(SharedLinkOtpWritableNone value) => new SharedLinkOtpWritable(value);
        
        public static implicit operator SharedLinkOtpWritable(SharedLinkOtpWritableOpen value) => new SharedLinkOtpWritable(value);
        
        public static implicit operator SharedLinkOtpWritable(SharedLinkOtpWritableAllowlist value) => new SharedLinkOtpWritable(value);
        
        class SharedLinkOtpWritableConverter : JsonConverter<SharedLinkOtpWritable> {
            public override SharedLinkOtpWritable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    try {
                        var result = JsonSerializer.Deserialize<SharedLinkOtpWritableNone>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SharedLinkOtpWritableOpen>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SharedLinkOtpWritableAllowlist>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, SharedLinkOtpWritable value, JsonSerializerOptions options) {
                if (value?.SharedLinkOtpWritableNone != null) {
                    JsonSerializer.Serialize(writer, value.SharedLinkOtpWritableNone, options);
                    return;
                }
                if (value?.SharedLinkOtpWritableOpen != null) {
                    JsonSerializer.Serialize(writer, value.SharedLinkOtpWritableOpen, options);
                    return;
                }
                if (value?.SharedLinkOtpWritableAllowlist != null) {
                    JsonSerializer.Serialize(writer, value.SharedLinkOtpWritableAllowlist, options);
                    return;
                }
            }

        }

    }
}