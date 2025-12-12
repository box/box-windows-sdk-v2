using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInputNumberWithPeriodValidation : ISerializable {
        /// <summary>
        /// Validates that the text input uses a number format with a period as the decimal separator (for example, 1.23).
        /// </summary>
        [JsonPropertyName("validation_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputNumberWithPeriodValidationValidationTypeField>))]
        public StringEnum<SignRequestSignerInputNumberWithPeriodValidationValidationTypeField> ValidationType { get; set; }

        public SignRequestSignerInputNumberWithPeriodValidation() {
            
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