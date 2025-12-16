using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInputZip4Validation : ISerializable {
        /// <summary>
        /// Validates that the text input is a ZIP+4 code.
        /// </summary>
        [JsonPropertyName("validation_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputZip4ValidationValidationTypeField>))]
        public StringEnum<SignRequestSignerInputZip4ValidationValidationTypeField> ValidationType { get; }

        public SignRequestSignerInputZip4Validation(SignRequestSignerInputZip4ValidationValidationTypeField validationType = SignRequestSignerInputZip4ValidationValidationTypeField.Zip4) {
            ValidationType = validationType;
        }
        
        [JsonConstructorAttribute]
        internal SignRequestSignerInputZip4Validation(StringEnum<SignRequestSignerInputZip4ValidationValidationTypeField> validationType) {
            ValidationType = SignRequestSignerInputZip4ValidationValidationTypeField.Zip4;
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