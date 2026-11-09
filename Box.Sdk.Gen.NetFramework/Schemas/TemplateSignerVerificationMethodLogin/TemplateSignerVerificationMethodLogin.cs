using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class TemplateSignerVerificationMethodLogin : ISerializable {
        /// <summary>
        /// The type of verification method.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TemplateSignerVerificationMethodLoginTypeField>))]
        public StringEnum<TemplateSignerVerificationMethodLoginTypeField> Type { get; set; }

        public TemplateSignerVerificationMethodLogin(TemplateSignerVerificationMethodLoginTypeField type = TemplateSignerVerificationMethodLoginTypeField.Login) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal TemplateSignerVerificationMethodLogin(StringEnum<TemplateSignerVerificationMethodLoginTypeField> type) {
            Type = TemplateSignerVerificationMethodLoginTypeField.Login;
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