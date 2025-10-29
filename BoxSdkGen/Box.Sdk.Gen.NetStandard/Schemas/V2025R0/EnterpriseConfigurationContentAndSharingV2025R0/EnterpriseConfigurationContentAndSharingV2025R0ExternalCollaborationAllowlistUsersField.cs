using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationAllowlistUsersField : EnterpriseConfigurationItemV2025R0, ISerializable {
        [JsonPropertyName("value")]
        public IReadOnlyList<ListUserV2025R0> Value { get; set; }

        public EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationAllowlistUsersField() {
            
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