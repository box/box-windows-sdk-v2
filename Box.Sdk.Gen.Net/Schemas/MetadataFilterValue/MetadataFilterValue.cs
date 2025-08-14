using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(MetadataFilterValueConverter))]
    public class MetadataFilterValue {
        internal OneOf<string, double, IReadOnlyList<string>, MetadataFieldFilterFloatRange, MetadataFieldFilterDateRange> _oneOf;
        
        public string? StringVal => _oneOf._val0;
        
        public double? DoubleVal => _oneOf._val1;
        
        public IReadOnlyList<string>? ListVal => _oneOf._val2;
        
        public MetadataFieldFilterFloatRange? MetadataFieldFilterFloatRange => _oneOf._val3;
        
        public MetadataFieldFilterDateRange? MetadataFieldFilterDateRange => _oneOf._val4;
        
        public MetadataFilterValue(string value) {_oneOf = new OneOf<string, double, IReadOnlyList<string>, MetadataFieldFilterFloatRange, MetadataFieldFilterDateRange>(value);}
        
        public MetadataFilterValue(double value) {_oneOf = new OneOf<string, double, IReadOnlyList<string>, MetadataFieldFilterFloatRange, MetadataFieldFilterDateRange>(value);}
        
        public MetadataFilterValue(IReadOnlyList<string> value) {_oneOf = new OneOf<string, double, IReadOnlyList<string>, MetadataFieldFilterFloatRange, MetadataFieldFilterDateRange>(value);}
        
        public MetadataFilterValue(MetadataFieldFilterFloatRange value) {_oneOf = new OneOf<string, double, IReadOnlyList<string>, MetadataFieldFilterFloatRange, MetadataFieldFilterDateRange>(value);}
        
        public MetadataFilterValue(MetadataFieldFilterDateRange value) {_oneOf = new OneOf<string, double, IReadOnlyList<string>, MetadataFieldFilterFloatRange, MetadataFieldFilterDateRange>(value);}
        
        public static implicit operator MetadataFilterValue(string value) => new MetadataFilterValue(value);
        
        public static implicit operator MetadataFilterValue(double value) => new MetadataFilterValue(value);
        
        public static implicit operator MetadataFilterValue(ReadOnlyCollection<string> value) => new MetadataFilterValue(value);
        
        public static implicit operator MetadataFilterValue(MetadataFieldFilterFloatRange value) => new MetadataFilterValue(value);
        
        public static implicit operator MetadataFilterValue(MetadataFieldFilterDateRange value) => new MetadataFilterValue(value);
        
        class MetadataFilterValueConverter : JsonConverter<MetadataFilterValue> {
            public override MetadataFilterValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                try {
                    var result = JsonSerializer.Deserialize<string>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return result;
                    }
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
                        return new MetadataFilterValue(result);
                    }
                } catch {
                    
                }
                try {
                    var result = JsonSerializer.Deserialize<MetadataFieldFilterFloatRange>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return result;
                    }
                } catch {
                    
                }
                try {
                    var result = JsonSerializer.Deserialize<MetadataFieldFilterDateRange>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return result;
                    }
                } catch {
                    
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, MetadataFilterValue? value, JsonSerializerOptions options) {
                if (value?.StringVal != null) {
                    JsonSerializer.Serialize(writer, value.StringVal, options);
                    return;
                }
                if (value?.ListVal != null) {
                    JsonSerializer.Serialize(writer, value.ListVal, options);
                    return;
                }
                if (value?.MetadataFieldFilterFloatRange != null) {
                    JsonSerializer.Serialize(writer, value.MetadataFieldFilterFloatRange, options);
                    return;
                }
                if (value?.MetadataFieldFilterDateRange != null) {
                    JsonSerializer.Serialize(writer, value.MetadataFieldFilterDateRange, options);
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