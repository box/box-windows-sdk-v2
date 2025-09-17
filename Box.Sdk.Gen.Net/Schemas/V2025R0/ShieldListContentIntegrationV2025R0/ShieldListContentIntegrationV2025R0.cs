using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldListContentIntegrationV2025R0 : ISerializable {
        /// <summary>
        /// The type of content in the shield list.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldListContentIntegrationV2025R0TypeField>))]
        public StringEnum<ShieldListContentIntegrationV2025R0TypeField> Type { get; }

        /// <summary>
        /// List of integration.
        /// </summary>
        [JsonPropertyName("integrations")]
        public IReadOnlyList<ShieldListContentIntegrationV2025R0IntegrationsField> Integrations { get; }

        public ShieldListContentIntegrationV2025R0(IReadOnlyList<ShieldListContentIntegrationV2025R0IntegrationsField> integrations, ShieldListContentIntegrationV2025R0TypeField type = ShieldListContentIntegrationV2025R0TypeField.Integration) {
            Type = type;
            Integrations = integrations;
        }
        
        [JsonConstructorAttribute]
        internal ShieldListContentIntegrationV2025R0(IReadOnlyList<ShieldListContentIntegrationV2025R0IntegrationsField> integrations, StringEnum<ShieldListContentIntegrationV2025R0TypeField> type) {
            Type = ShieldListContentIntegrationV2025R0TypeField.Integration;
            Integrations = integrations;
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