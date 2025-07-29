using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingTeams : IntegrationMappingBase, ISerializable {
        /// <summary>
        /// Identifies the Box partner app,
        /// with which the mapping is associated.
        /// Supports Slack and Teams.
        /// (part of the composite key together with `id`).
        /// </summary>
        [JsonPropertyName("integration_type")]
        [JsonConverter(typeof(StringEnumConverter<IntegrationMappingTeamsIntegrationTypeField>))]
        public StringEnum<IntegrationMappingTeamsIntegrationTypeField>? IntegrationType { get; init; }

        /// <summary>
        /// Identifies whether the mapping has
        /// been manually set by the team owner from UI for channels
        /// (as opposed to being automatically created).
        /// </summary>
        [JsonPropertyName("is_overridden_by_manual_mapping")]
        public bool? IsOverriddenByManualMapping { get; init; }

        /// <summary>
        /// Mapped item object for Teams.
        /// </summary>
        [JsonPropertyName("partner_item")]
        public IntegrationMappingPartnerItemTeamsUnion PartnerItem { get; }

        [JsonPropertyName("box_item")]
        public FolderReference BoxItem { get; }

        /// <summary>
        /// When the integration mapping object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// When the integration mapping object was last modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        public IntegrationMappingTeams(string id, IntegrationMappingPartnerItemTeamsUnion partnerItem, FolderReference boxItem, IntegrationMappingBaseTypeField type = IntegrationMappingBaseTypeField.IntegrationMapping) : base(id, type) {
            PartnerItem = partnerItem;
            BoxItem = boxItem;
        }
        
        [JsonConstructorAttribute]
        internal IntegrationMappingTeams(string id, IntegrationMappingPartnerItemTeamsUnion partnerItem, FolderReference boxItem, StringEnum<IntegrationMappingBaseTypeField> type) : base(id, type ?? new StringEnum<IntegrationMappingBaseTypeField>(IntegrationMappingBaseTypeField.IntegrationMapping)) {
            PartnerItem = partnerItem;
            BoxItem = boxItem;
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