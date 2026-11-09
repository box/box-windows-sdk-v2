using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(SignRequestSignerVerificationMethodConverter))]
    public class SignRequestSignerVerificationMethod {
        internal OneOf<SignRequestSignerVerificationMethodLogin?, SignRequestSignerVerificationMethodPhoneSms?, SignRequestSignerVerificationMethodCacPiv?> _oneOf;
        
        public SignRequestSignerVerificationMethodLogin? SignRequestSignerVerificationMethodLogin => _oneOf._val0;
        
        public SignRequestSignerVerificationMethodPhoneSms? SignRequestSignerVerificationMethodPhoneSms => _oneOf._val1;
        
        public SignRequestSignerVerificationMethodCacPiv? SignRequestSignerVerificationMethodCacPiv => _oneOf._val2;
        
        public SignRequestSignerVerificationMethod(SignRequestSignerVerificationMethodLogin value) {_oneOf = new OneOf<SignRequestSignerVerificationMethodLogin?, SignRequestSignerVerificationMethodPhoneSms?, SignRequestSignerVerificationMethodCacPiv?>(value);}
        
        public SignRequestSignerVerificationMethod(SignRequestSignerVerificationMethodPhoneSms value) {_oneOf = new OneOf<SignRequestSignerVerificationMethodLogin?, SignRequestSignerVerificationMethodPhoneSms?, SignRequestSignerVerificationMethodCacPiv?>(value);}
        
        public SignRequestSignerVerificationMethod(SignRequestSignerVerificationMethodCacPiv value) {_oneOf = new OneOf<SignRequestSignerVerificationMethodLogin?, SignRequestSignerVerificationMethodPhoneSms?, SignRequestSignerVerificationMethodCacPiv?>(value);}
        
        public static implicit operator SignRequestSignerVerificationMethod(SignRequestSignerVerificationMethodLogin value) => new SignRequestSignerVerificationMethod(value);
        
        public static implicit operator SignRequestSignerVerificationMethod(SignRequestSignerVerificationMethodPhoneSms value) => new SignRequestSignerVerificationMethod(value);
        
        public static implicit operator SignRequestSignerVerificationMethod(SignRequestSignerVerificationMethodCacPiv value) => new SignRequestSignerVerificationMethod(value);
        
        class SignRequestSignerVerificationMethodConverter : JsonConverter<SignRequestSignerVerificationMethod> {
            public override SignRequestSignerVerificationMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "login":
                            return JsonSerializer.Deserialize<SignRequestSignerVerificationMethodLogin>(document) ?? throw new Exception($"Could not deserialize {document} to SignRequestSignerVerificationMethodLogin");
                        case "phone":
                            return JsonSerializer.Deserialize<SignRequestSignerVerificationMethodPhoneSms>(document) ?? throw new Exception($"Could not deserialize {document} to SignRequestSignerVerificationMethodPhoneSms");
                        case "cac_piv":
                            return JsonSerializer.Deserialize<SignRequestSignerVerificationMethodCacPiv>(document) ?? throw new Exception($"Could not deserialize {document} to SignRequestSignerVerificationMethodCacPiv");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, SignRequestSignerVerificationMethod? value, JsonSerializerOptions options) {
                if (value?.SignRequestSignerVerificationMethodLogin != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerVerificationMethodLogin, options);
                    return;
                }
                if (value?.SignRequestSignerVerificationMethodPhoneSms != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerVerificationMethodPhoneSms, options);
                    return;
                }
                if (value?.SignRequestSignerVerificationMethodCacPiv != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerVerificationMethodCacPiv, options);
                    return;
                }
            }

        }

    }
}