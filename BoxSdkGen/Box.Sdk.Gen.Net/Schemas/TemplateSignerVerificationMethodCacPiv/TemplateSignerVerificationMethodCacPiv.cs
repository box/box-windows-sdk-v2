using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class TemplateSignerVerificationMethodCacPiv : ISerializable {
        /// <summary>
        /// The type of verification method.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TemplateSignerVerificationMethodCacPivTypeField>))]
        public StringEnum<TemplateSignerVerificationMethodCacPivTypeField> Type { get; }

        public TemplateSignerVerificationMethodCacPiv(TemplateSignerVerificationMethodCacPivTypeField type = TemplateSignerVerificationMethodCacPivTypeField.CacPiv) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal TemplateSignerVerificationMethodCacPiv(StringEnum<TemplateSignerVerificationMethodCacPivTypeField> type) {
            Type = TemplateSignerVerificationMethodCacPivTypeField.CacPiv;
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