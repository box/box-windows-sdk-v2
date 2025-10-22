using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusField : EnterpriseConfigurationItemV2025R0, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isvalueSet")]
        protected bool _isValueSet { get; set; }

        protected StringEnum<EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusFieldValueField>? _value { get; set; }

        /// <summary>
        /// The external collaboration status.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonConverter(typeof(StringEnumConverter<EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusFieldValueField>))]
        public StringEnum<EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusFieldValueField>? Value { get => _value; init { _value = value; _isValueSet = true; } }

        public EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusField() {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}