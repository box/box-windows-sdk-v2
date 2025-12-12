using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(SignRequestSignerInputValidationConverter))]
    public class SignRequestSignerInputValidation {
        internal OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation> _oneOf;
        
        public SignRequestSignerInputEmailValidation SignRequestSignerInputEmailValidation => _oneOf._val0;
        
        public SignRequestSignerInputCustomValidation SignRequestSignerInputCustomValidation => _oneOf._val1;
        
        public SignRequestSignerInputZipValidation SignRequestSignerInputZipValidation => _oneOf._val2;
        
        public SignRequestSignerInputZip4Validation SignRequestSignerInputZip4Validation => _oneOf._val3;
        
        public SignRequestSignerInputSsnValidation SignRequestSignerInputSsnValidation => _oneOf._val4;
        
        public SignRequestSignerInputNumberWithPeriodValidation SignRequestSignerInputNumberWithPeriodValidation => _oneOf._val5;
        
        public SignRequestSignerInputNumberWithCommaValidation SignRequestSignerInputNumberWithCommaValidation => _oneOf._val6;
        
        public SignRequestSignerInputDateIsoValidation SignRequestSignerInputDateIsoValidation => _oneOf._val7;
        
        public SignRequestSignerInputDateUsValidation SignRequestSignerInputDateUsValidation => _oneOf._val8;
        
        public SignRequestSignerInputDateEuValidation SignRequestSignerInputDateEuValidation => _oneOf._val9;
        
        public SignRequestSignerInputDateAsiaValidation SignRequestSignerInputDateAsiaValidation => _oneOf._val10;
        
        public SignRequestSignerInputValidation(SignRequestSignerInputEmailValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputCustomValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputZipValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputZip4Validation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputSsnValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputNumberWithPeriodValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputNumberWithCommaValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputDateIsoValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputDateUsValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputDateEuValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public SignRequestSignerInputValidation(SignRequestSignerInputDateAsiaValidation value) {_oneOf = new OneOf<SignRequestSignerInputEmailValidation, SignRequestSignerInputCustomValidation, SignRequestSignerInputZipValidation, SignRequestSignerInputZip4Validation, SignRequestSignerInputSsnValidation, SignRequestSignerInputNumberWithPeriodValidation, SignRequestSignerInputNumberWithCommaValidation, SignRequestSignerInputDateIsoValidation, SignRequestSignerInputDateUsValidation, SignRequestSignerInputDateEuValidation, SignRequestSignerInputDateAsiaValidation>(value);}
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputEmailValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputCustomValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputZipValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputZip4Validation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputSsnValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputNumberWithPeriodValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputNumberWithCommaValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputDateIsoValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputDateUsValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputDateEuValidation value) => new SignRequestSignerInputValidation(value);
        
        public static implicit operator SignRequestSignerInputValidation(SignRequestSignerInputDateAsiaValidation value) => new SignRequestSignerInputValidation(value);
        
        class SignRequestSignerInputValidationConverter : JsonConverter<SignRequestSignerInputValidation> {
            public override SignRequestSignerInputValidation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputEmailValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputCustomValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputZipValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputZip4Validation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputSsnValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputNumberWithPeriodValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputNumberWithCommaValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputDateIsoValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputDateUsValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputDateEuValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    try {
                        var result = JsonSerializer.Deserialize<SignRequestSignerInputDateAsiaValidation>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                        if (result != null) {
                            return result;
                        }
                    } catch {
                        
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, SignRequestSignerInputValidation value, JsonSerializerOptions options) {
                if (value?.SignRequestSignerInputEmailValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputEmailValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputCustomValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputCustomValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputZipValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputZipValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputZip4Validation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputZip4Validation, options);
                    return;
                }
                if (value?.SignRequestSignerInputSsnValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputSsnValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputNumberWithPeriodValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputNumberWithPeriodValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputNumberWithCommaValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputNumberWithCommaValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputDateIsoValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputDateIsoValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputDateUsValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputDateUsValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputDateEuValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputDateEuValidation, options);
                    return;
                }
                if (value?.SignRequestSignerInputDateAsiaValidation != null) {
                    JsonSerializer.Serialize(writer, value.SignRequestSignerInputDateAsiaValidation, options);
                    return;
                }
            }

        }

    }
}