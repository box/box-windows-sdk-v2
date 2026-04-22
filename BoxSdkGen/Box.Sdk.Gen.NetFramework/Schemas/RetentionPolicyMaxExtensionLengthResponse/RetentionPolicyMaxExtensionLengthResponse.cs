using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(RetentionPolicyMaxExtensionLengthResponseConverter))]
    public class RetentionPolicyMaxExtensionLengthResponse {
        internal OneOf<RetentionPolicyMaxExtensionLengthResponseEnum?, string> _oneOf;
        
        public RetentionPolicyMaxExtensionLengthResponseEnum? RetentionPolicyMaxExtensionLengthResponseEnum => _oneOf._val0;
        
        public string StringVal => _oneOf._val1;
        
        public RetentionPolicyMaxExtensionLengthResponse(RetentionPolicyMaxExtensionLengthResponseEnum value) {_oneOf = new OneOf<RetentionPolicyMaxExtensionLengthResponseEnum?, string>(value);}
        
        public RetentionPolicyMaxExtensionLengthResponse(string value) {_oneOf = new OneOf<RetentionPolicyMaxExtensionLengthResponseEnum?, string>(value);}
        
        public static implicit operator RetentionPolicyMaxExtensionLengthResponse(RetentionPolicyMaxExtensionLengthResponseEnum value) => new RetentionPolicyMaxExtensionLengthResponse(value);
        
        public static implicit operator RetentionPolicyMaxExtensionLengthResponse(string value) => new RetentionPolicyMaxExtensionLengthResponse(value);
        
        class RetentionPolicyMaxExtensionLengthResponseConverter : JsonConverter<RetentionPolicyMaxExtensionLengthResponse> {
            public override RetentionPolicyMaxExtensionLengthResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    try {
                        var result = JsonSerializer.Deserialize<StringEnum<RetentionPolicyMaxExtensionLengthResponseEnum>>(document, new JsonSerializerOptions() { Converters = { new Box.Sdk.Gen.Internal.StringEnumConverter<RetentionPolicyMaxExtensionLengthResponseEnum>() } });
                        if (result?.Value != null) {
                            return (RetentionPolicyMaxExtensionLengthResponseEnum)result.Value;
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
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, RetentionPolicyMaxExtensionLengthResponse value, JsonSerializerOptions options) {
                if (value?.RetentionPolicyMaxExtensionLengthResponseEnum != null) {
                    JsonSerializer.Serialize(writer, new StringEnum<RetentionPolicyMaxExtensionLengthResponseEnum>(value.RetentionPolicyMaxExtensionLengthResponseEnum).StringValue, options);
                    return;
                }
                if (value?.StringVal != null) {
                    JsonSerializer.Serialize(writer, value.StringVal, options);
                    return;
                }
            }

        }

    }
}