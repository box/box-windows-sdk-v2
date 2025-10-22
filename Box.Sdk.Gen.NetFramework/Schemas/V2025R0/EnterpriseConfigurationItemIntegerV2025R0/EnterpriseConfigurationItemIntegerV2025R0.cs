using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationItemIntegerV2025R0 : EnterpriseConfigurationItemV2025R0, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isvalueSet")]
        protected bool _isValueSet { get; set; }

        protected long? _value { get; set; }

        /// <summary>
        /// The value of the enterprise configuration as an integer.
        /// </summary>
        [JsonPropertyName("value")]
        public long? Value { get => _value; set { _value = value; _isValueSet = true; } }

        public EnterpriseConfigurationItemIntegerV2025R0() {
            
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