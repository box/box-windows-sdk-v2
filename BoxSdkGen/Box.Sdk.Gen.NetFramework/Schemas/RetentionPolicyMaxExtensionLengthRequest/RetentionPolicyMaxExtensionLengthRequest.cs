using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(RetentionPolicyMaxExtensionLengthRequestConverter))]
    public class RetentionPolicyMaxExtensionLengthRequest {
        internal OneOf<RetentionPolicyMaxExtensionLengthRequestEnum?, string, int?> _oneOf;
        
        public RetentionPolicyMaxExtensionLengthRequestEnum? RetentionPolicyMaxExtensionLengthRequestEnum => _oneOf._val0;
        
        public string StringVal => _oneOf._val1;
        
        public int? IntVal => _oneOf._val2;
        
        public RetentionPolicyMaxExtensionLengthRequest(RetentionPolicyMaxExtensionLengthRequestEnum value) {_oneOf = new OneOf<RetentionPolicyMaxExtensionLengthRequestEnum?, string, int?>(value);}
        
        public RetentionPolicyMaxExtensionLengthRequest(string value) {_oneOf = new OneOf<RetentionPolicyMaxExtensionLengthRequestEnum?, string, int?>(value);}
        
        public RetentionPolicyMaxExtensionLengthRequest(int value) {_oneOf = new OneOf<RetentionPolicyMaxExtensionLengthRequestEnum?, string, int?>(value);}
        
        public static implicit operator RetentionPolicyMaxExtensionLengthRequest(RetentionPolicyMaxExtensionLengthRequestEnum value) => new RetentionPolicyMaxExtensionLengthRequest(value);
        
        public static implicit operator RetentionPolicyMaxExtensionLengthRequest(string value) => new RetentionPolicyMaxExtensionLengthRequest(value);
        
        public static implicit operator RetentionPolicyMaxExtensionLengthRequest(int value) => new RetentionPolicyMaxExtensionLengthRequest(value);
        
        class RetentionPolicyMaxExtensionLengthRequestConverter : JsonConverter<RetentionPolicyMaxExtensionLengthRequest> {
            public override RetentionPolicyMaxExtensionLengthRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    try {
                        var result = JsonSerializer.Deserialize<StringEnum<RetentionPolicyMaxExtensionLengthRequestEnum>>(document, new JsonSerializerOptions() { Converters = { new Box.Sdk.Gen.Internal.StringEnumConverter<RetentionPolicyMaxExtensionLengthRequestEnum>() } });
                        if (result?.Value != null) {
                            return (RetentionPolicyMaxExtensionLengthRequestEnum)result.Value;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<string>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<int>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        return result;
                    } catch {
                        
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, RetentionPolicyMaxExtensionLengthRequest value, JsonSerializerOptions options) {
                if (value?.RetentionPolicyMaxExtensionLengthRequestEnum != null) {
                    JsonSerializer.Serialize(writer, new StringEnum<RetentionPolicyMaxExtensionLengthRequestEnum>(value.RetentionPolicyMaxExtensionLengthRequestEnum).StringValue, options);
                    return;
                }
                if (value?.StringVal != null) {
                    JsonSerializer.Serialize(writer, value.StringVal, options);
                    return;
                }
                if (value?.IntVal != null) {
                    JsonSerializer.Serialize(writer, value.IntVal, options);
                    return;
                }
            }

        }

    }
}