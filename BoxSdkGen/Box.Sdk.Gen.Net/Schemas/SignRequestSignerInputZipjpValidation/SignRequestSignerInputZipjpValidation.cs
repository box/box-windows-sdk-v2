using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInputZipjpValidation : ISerializable {
        /// <summary>
        /// Validates that the text input is a Japanese ZIP code.
        /// </summary>
        [JsonPropertyName("validation_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputZipjpValidationValidationTypeField>))]
        public StringEnum<SignRequestSignerInputZipjpValidationValidationTypeField> ValidationType { get; }

        public SignRequestSignerInputZipjpValidation(SignRequestSignerInputZipjpValidationValidationTypeField validationType = SignRequestSignerInputZipjpValidationValidationTypeField.ZipJp) {
            ValidationType = validationType;
        }
        
        [JsonConstructorAttribute]
        internal SignRequestSignerInputZipjpValidation(StringEnum<SignRequestSignerInputZipjpValidationValidationTypeField> validationType) {
            ValidationType = SignRequestSignerInputZipjpValidationValidationTypeField.ZipJp;
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