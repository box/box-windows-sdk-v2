using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusField : EnterpriseConfigurationItemV2025R0, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isvalueSet")]
        protected bool _isValueSet { get; set; }

        protected string _value { get; set; }

        /// <summary>
        /// The external collaboration status.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get => _value; set { _value = value; _isValueSet = true; } }

        public EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusField() {
            
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