using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingPartnerItemTeams : ISerializable {
        /// <summary>
        /// Type of the mapped item referenced in `id`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<IntegrationMappingPartnerItemTeamsTypeField>))]
        public StringEnum<IntegrationMappingPartnerItemTeamsTypeField> Type { get; }

        /// <summary>
        /// ID of the mapped item (of type referenced in `type`).
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// ID of the tenant that is registered with Microsoft Teams.
        /// </summary>
        [JsonPropertyName("tenant_id")]
        public string TenantId { get; }

        public IntegrationMappingPartnerItemTeams(IntegrationMappingPartnerItemTeamsTypeField type, string id, string tenantId) {
            Type = type;
            Id = id;
            TenantId = tenantId;
        }
        
        [JsonConstructorAttribute]
        internal IntegrationMappingPartnerItemTeams(StringEnum<IntegrationMappingPartnerItemTeamsTypeField> type, string id, string tenantId) {
            Type = type;
            Id = id;
            TenantId = tenantId;
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