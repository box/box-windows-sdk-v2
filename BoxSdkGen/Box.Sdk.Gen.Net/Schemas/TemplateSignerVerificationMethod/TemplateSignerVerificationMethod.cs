using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(TemplateSignerVerificationMethodConverter))]
    public class TemplateSignerVerificationMethod {
        internal OneOf<TemplateSignerVerificationMethodLogin?, TemplateSignerVerificationMethodPhoneSms?, TemplateSignerVerificationMethodCacPiv?> _oneOf;
        
        public TemplateSignerVerificationMethodLogin? TemplateSignerVerificationMethodLogin => _oneOf._val0;
        
        public TemplateSignerVerificationMethodPhoneSms? TemplateSignerVerificationMethodPhoneSms => _oneOf._val1;
        
        public TemplateSignerVerificationMethodCacPiv? TemplateSignerVerificationMethodCacPiv => _oneOf._val2;
        
        public TemplateSignerVerificationMethod(TemplateSignerVerificationMethodLogin value) {_oneOf = new OneOf<TemplateSignerVerificationMethodLogin?, TemplateSignerVerificationMethodPhoneSms?, TemplateSignerVerificationMethodCacPiv?>(value);}
        
        public TemplateSignerVerificationMethod(TemplateSignerVerificationMethodPhoneSms value) {_oneOf = new OneOf<TemplateSignerVerificationMethodLogin?, TemplateSignerVerificationMethodPhoneSms?, TemplateSignerVerificationMethodCacPiv?>(value);}
        
        public TemplateSignerVerificationMethod(TemplateSignerVerificationMethodCacPiv value) {_oneOf = new OneOf<TemplateSignerVerificationMethodLogin?, TemplateSignerVerificationMethodPhoneSms?, TemplateSignerVerificationMethodCacPiv?>(value);}
        
        public static implicit operator TemplateSignerVerificationMethod(TemplateSignerVerificationMethodLogin value) => new TemplateSignerVerificationMethod(value);
        
        public static implicit operator TemplateSignerVerificationMethod(TemplateSignerVerificationMethodPhoneSms value) => new TemplateSignerVerificationMethod(value);
        
        public static implicit operator TemplateSignerVerificationMethod(TemplateSignerVerificationMethodCacPiv value) => new TemplateSignerVerificationMethod(value);
        
        class TemplateSignerVerificationMethodConverter : JsonConverter<TemplateSignerVerificationMethod> {
            public override TemplateSignerVerificationMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "login":
                            return JsonSerializer.Deserialize<TemplateSignerVerificationMethodLogin>(document) ?? throw new Exception($"Could not deserialize {document} to TemplateSignerVerificationMethodLogin");
                        case "phone":
                            return JsonSerializer.Deserialize<TemplateSignerVerificationMethodPhoneSms>(document) ?? throw new Exception($"Could not deserialize {document} to TemplateSignerVerificationMethodPhoneSms");
                        case "cac_piv":
                            return JsonSerializer.Deserialize<TemplateSignerVerificationMethodCacPiv>(document) ?? throw new Exception($"Could not deserialize {document} to TemplateSignerVerificationMethodCacPiv");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, TemplateSignerVerificationMethod? value, JsonSerializerOptions options) {
                if (value?.TemplateSignerVerificationMethodLogin != null) {
                    JsonSerializer.Serialize(writer, value.TemplateSignerVerificationMethodLogin, options);
                    return;
                }
                if (value?.TemplateSignerVerificationMethodPhoneSms != null) {
                    JsonSerializer.Serialize(writer, value.TemplateSignerVerificationMethodPhoneSms, options);
                    return;
                }
                if (value?.TemplateSignerVerificationMethodCacPiv != null) {
                    JsonSerializer.Serialize(writer, value.TemplateSignerVerificationMethodCacPiv, options);
                    return;
                }
            }

        }

    }
}