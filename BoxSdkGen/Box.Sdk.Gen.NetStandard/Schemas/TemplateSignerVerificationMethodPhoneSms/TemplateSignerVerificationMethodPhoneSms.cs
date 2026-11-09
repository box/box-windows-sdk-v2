using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class TemplateSignerVerificationMethodPhoneSms : ISerializable {
        /// <summary>
        /// The type of verification method.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TemplateSignerVerificationMethodPhoneSmsTypeField>))]
        public StringEnum<TemplateSignerVerificationMethodPhoneSmsTypeField> Type { get; set; }

        public TemplateSignerVerificationMethodPhoneSms(TemplateSignerVerificationMethodPhoneSmsTypeField type = TemplateSignerVerificationMethodPhoneSmsTypeField.Phone) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal TemplateSignerVerificationMethodPhoneSms(StringEnum<TemplateSignerVerificationMethodPhoneSmsTypeField> type) {
            Type = TemplateSignerVerificationMethodPhoneSmsTypeField.Phone;
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