using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(MetadataInstanceValueConverter))]
    public class MetadataInstanceValue : OneOf<string, long, double, IReadOnlyList<string>> {
        public string? StringVal => _val0;
        
        public long? LongVal => _val1;
        
        public double? DoubleVal => _val2;
        
        public IReadOnlyList<string>? ListVal => _val3;
        
        public MetadataInstanceValue(string value) : base(value) {}
        
        public MetadataInstanceValue(long value) : base(value) {}
        
        public MetadataInstanceValue(double value) : base(value) {}
        
        public MetadataInstanceValue(IReadOnlyList<string> value) : base(value) {}
        
        public static implicit operator MetadataInstanceValue(string value) => new MetadataInstanceValue(value);
        
        public static implicit operator MetadataInstanceValue(long value) => new MetadataInstanceValue(value);
        
        public static implicit operator MetadataInstanceValue(double value) => new MetadataInstanceValue(value);
        
        public static implicit operator MetadataInstanceValue(ReadOnlyCollection<string> value) => new MetadataInstanceValue(value);
        
        class MetadataInstanceValueConverter : JsonConverter<MetadataInstanceValue> {
            public override MetadataInstanceValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
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
                try {
                    var result = JsonSerializer.Deserialize<double>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    return result;
                } catch {
                    
                }
                try {
                    var result = JsonSerializer.Deserialize<IReadOnlyList<string>>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return new MetadataInstanceValue(result);
                    }
                } catch {
                    
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, MetadataInstanceValue? value, JsonSerializerOptions options) {
                if (value?.StringVal != null) {
                    JsonSerializer.Serialize(writer, value.StringVal, options);
                    return;
                }
                if (value?.ListVal != null) {
                    JsonSerializer.Serialize(writer, value.ListVal, options);
                    return;
                }
                if (value?.LongVal != null) {
                    JsonSerializer.Serialize(writer, value.LongVal, options);
                    return;
                }
                if (value?.DoubleVal != null) {
                    JsonSerializer.Serialize(writer, value.DoubleVal, options);
                    return;
                }
            }

        }

    }
}