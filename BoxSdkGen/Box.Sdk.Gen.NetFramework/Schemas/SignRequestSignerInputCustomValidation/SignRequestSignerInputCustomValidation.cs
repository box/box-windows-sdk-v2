using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInputCustomValidation : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscustom_regexSet")]
        protected bool _isCustomRegexSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscustom_error_messageSet")]
        protected bool _isCustomErrorMessageSet { get; set; }

        protected string _customRegex { get; set; }

        protected string _customErrorMessage { get; set; }

        /// <summary>
        /// Defines the validation format for the text input as custom.
        /// A custom regular expression is used for validation.
        /// </summary>
        [JsonPropertyName("validation_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputCustomValidationValidationTypeField>))]
        public StringEnum<SignRequestSignerInputCustomValidationValidationTypeField> ValidationType { get; set; }

        /// <summary>
        /// Regular expression used for validation.
        /// </summary>
        [JsonPropertyName("custom_regex")]
        public string CustomRegex { get => _customRegex; set { _customRegex = value; _isCustomRegexSet = true; } }

        /// <summary>
        /// Error message shown if input fails custom regular expression validation.
        /// </summary>
        [JsonPropertyName("custom_error_message")]
        public string CustomErrorMessage { get => _customErrorMessage; set { _customErrorMessage = value; _isCustomErrorMessageSet = true; } }

        public SignRequestSignerInputCustomValidation(SignRequestSignerInputCustomValidationValidationTypeField validationType = SignRequestSignerInputCustomValidationValidationTypeField.Custom) {
            ValidationType = validationType;
        }
        
        [JsonConstructorAttribute]
        internal SignRequestSignerInputCustomValidation(StringEnum<SignRequestSignerInputCustomValidationValidationTypeField> validationType) {
            ValidationType = SignRequestSignerInputCustomValidationValidationTypeField.Custom;
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