using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInputDateAsiaValidation : ISerializable {
        /// <summary>
        /// Validates that the text input uses the Asian date format `YYYY/MM/DD`.
        /// </summary>
        [JsonPropertyName("validation_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputDateAsiaValidationValidationTypeField>))]
        public StringEnum<SignRequestSignerInputDateAsiaValidationValidationTypeField> ValidationType { get; set; }

        public SignRequestSignerInputDateAsiaValidation() {
            
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