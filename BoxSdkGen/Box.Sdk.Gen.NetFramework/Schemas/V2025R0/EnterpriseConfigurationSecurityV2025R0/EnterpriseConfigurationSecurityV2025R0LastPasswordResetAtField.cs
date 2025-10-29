using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationSecurityV2025R0LastPasswordResetAtField : EnterpriseConfigurationItemV2025R0, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isvalueSet")]
        protected bool _isValueSet { get; set; }

        protected System.DateTimeOffset? _value { get; set; }

        /// <summary>
        /// When an enterprise password reset was last applied.
        /// </summary>
        [JsonPropertyName("value")]
        public System.DateTimeOffset? Value { get => _value; set { _value = value; _isValueSet = true; } }

        public EnterpriseConfigurationSecurityV2025R0LastPasswordResetAtField() {
            
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}