using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationV2025R0 : ISerializable {
        /// <summary>
        /// The identifier of the enterprise configuration which is the ID of the enterprise.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `enterprise_configuration`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<EnterpriseConfigurationV2025R0TypeField>))]
        public StringEnum<EnterpriseConfigurationV2025R0TypeField>? Type { get; init; }

        [JsonPropertyName("security")]
        public EnterpriseConfigurationSecurityV2025R0? Security { get; init; }

        [JsonPropertyName("content_and_sharing")]
        public EnterpriseConfigurationContentAndSharingV2025R0? ContentAndSharing { get; init; }

        [JsonPropertyName("user_settings")]
        public EnterpriseConfigurationUserSettingsV2025R0? UserSettings { get; init; }

        [JsonPropertyName("shield")]
        public EnterpriseConfigurationShieldV2025R0? Shield { get; init; }

        public EnterpriseConfigurationV2025R0() {
            
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