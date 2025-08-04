using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrStringConverter))]
    public class MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString : OneOf<MetadataFieldFilterDateRange, MetadataFieldFilterFloatRange, IReadOnlyList<string>, double, string> {
        public MetadataFieldFilterDateRange? MetadataFieldFilterDateRange => _val0;
        
        public MetadataFieldFilterFloatRange? MetadataFieldFilterFloatRange => _val1;
        
        public IReadOnlyList<string>? ListVal => _val2;
        
        public double? DoubleVal => _val3;
        
        public string? StringVal => _val4;
        
        public MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(MetadataFieldFilterDateRange value) : base(value) {}
        
        public MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(MetadataFieldFilterFloatRange value) : base(value) {}
        
        public MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(IReadOnlyList<string> value) : base(value) {}
        
        public MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(double value) : base(value) {}
        
        public MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(string value) : base(value) {}
        
        public static implicit operator MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(MetadataFieldFilterDateRange value) => new MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(value);
        
        public static implicit operator MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(MetadataFieldFilterFloatRange value) => new MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(value);
        
        public static implicit operator MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(ReadOnlyCollection<string> value) => new MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(value);
        
        public static implicit operator MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(double value) => new MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(value);
        
        public static implicit operator MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(string value) => new MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(value);
        
        class MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrStringConverter : JsonConverter<MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString> {
            public override MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                try {
                    var result = JsonSerializer.Deserialize<MetadataFieldFilterDateRange>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return result;
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
                    var result = JsonSerializer.Deserialize<IReadOnlyList<string>>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return new MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString(result);
                    }
                } catch {
                    
                }
                try {
                    var result = JsonSerializer.Deserialize<double>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    return result;
                } catch {
                    
                }
                try {
                    var result = JsonSerializer.Deserialize<string>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return result;
                    }
                } catch {
                    
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString? value, JsonSerializerOptions options) {
                if (value?.MetadataFieldFilterDateRange != null) {
                    JsonSerializer.Serialize(writer, value.MetadataFieldFilterDateRange, options);
                    return;
                }
                if (value?.MetadataFieldFilterFloatRange != null) {
                    JsonSerializer.Serialize(writer, value.MetadataFieldFilterFloatRange, options);
                    return;
                }
                if (value?.ListVal != null) {
                    JsonSerializer.Serialize(writer, value.ListVal, options);
                    return;
                }
                if (value?.StringVal != null) {
                    JsonSerializer.Serialize(writer, value.StringVal, options);
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