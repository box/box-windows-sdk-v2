using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInputEmailValidation : ISerializable {
        /// <summary>
        /// Validates that the text input is an email address.
        /// </summary>
        [JsonPropertyName("validation_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputEmailValidationValidationTypeField>))]
        public StringEnum<SignRequestSignerInputEmailValidationValidationTypeField> ValidationType { get; }

        public SignRequestSignerInputEmailValidation(SignRequestSignerInputEmailValidationValidationTypeField validationType = SignRequestSignerInputEmailValidationValidationTypeField.Email) {
            ValidationType = validationType;
        }
        
        [JsonConstructorAttribute]
        internal SignRequestSignerInputEmailValidation(StringEnum<SignRequestSignerInputEmailValidationValidationTypeField> validationType) {
            ValidationType = SignRequestSignerInputEmailValidationValidationTypeField.Email;
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}