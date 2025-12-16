using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInputSsnValidation : ISerializable {
        /// <summary>
        /// Validates that the text input is a Social Security Number (SSN).
        /// </summary>
        [JsonPropertyName("validation_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputSsnValidationValidationTypeField>))]
        public StringEnum<SignRequestSignerInputSsnValidationValidationTypeField> ValidationType { get; set; }

        public SignRequestSignerInputSsnValidation(SignRequestSignerInputSsnValidationValidationTypeField validationType = SignRequestSignerInputSsnValidationValidationTypeField.Ssn) {
            ValidationType = validationType;
        }
        
        [JsonConstructorAttribute]
        internal SignRequestSignerInputSsnValidation(StringEnum<SignRequestSignerInputSsnValidationValidationTypeField> validationType) {
            ValidationType = SignRequestSignerInputSsnValidationValidationTypeField.Ssn;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}