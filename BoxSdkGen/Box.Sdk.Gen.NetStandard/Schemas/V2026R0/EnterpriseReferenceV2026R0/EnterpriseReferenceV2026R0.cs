using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseReferenceV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier for this enterprise.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `enterprise`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<EnterpriseReferenceV2026R0TypeField>))]
        public StringEnum<EnterpriseReferenceV2026R0TypeField> Type { get; set; }

        public EnterpriseReferenceV2026R0(string id, EnterpriseReferenceV2026R0TypeField type = EnterpriseReferenceV2026R0TypeField.Enterprise) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal EnterpriseReferenceV2026R0(string id, StringEnum<EnterpriseReferenceV2026R0TypeField> type) {
            Id = id;
            Type = EnterpriseReferenceV2026R0TypeField.Enterprise;
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