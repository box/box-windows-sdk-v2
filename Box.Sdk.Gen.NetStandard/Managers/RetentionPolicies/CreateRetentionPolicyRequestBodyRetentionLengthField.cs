using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    [JsonConverter(typeof(CreateRetentionPolicyRequestBodyRetentionLengthFieldConverter))]
    public class CreateRetentionPolicyRequestBodyRetentionLengthField {
        internal OneOf<string, int?> _oneOf;
        
        public string StringVal => _oneOf._val0;
        
        public int? IntVal => _oneOf._val1;
        
        public CreateRetentionPolicyRequestBodyRetentionLengthField(string value) {_oneOf = new OneOf<string, int?>(value);}
        
        public CreateRetentionPolicyRequestBodyRetentionLengthField(int value) {_oneOf = new OneOf<string, int?>(value);}
        
        public static implicit operator CreateRetentionPolicyRequestBodyRetentionLengthField(string value) => new CreateRetentionPolicyRequestBodyRetentionLengthField(value);
        
        public static implicit operator CreateRetentionPolicyRequestBodyRetentionLengthField(int value) => new CreateRetentionPolicyRequestBodyRetentionLengthField(value);
        
        class CreateRetentionPolicyRequestBodyRetentionLengthFieldConverter : JsonConverter<CreateRetentionPolicyRequestBodyRetentionLengthField> {
            public override CreateRetentionPolicyRequestBodyRetentionLengthField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
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

            public override void Write(Utf8JsonWriter writer, CreateRetentionPolicyRequestBodyRetentionLengthField value, JsonSerializerOptions options) {
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