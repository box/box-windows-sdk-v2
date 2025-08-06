using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMapping : IntegrationMappingBase, ISerializable {
        /// <summary>
        /// Identifies the Box partner app,
        /// with which the mapping is associated.
        /// Currently only supports Slack.
        /// (part of the composite key together with `id`).
        /// </summary>
        [JsonPropertyName("integration_type")]
        [JsonConverter(typeof(StringEnumConverter<IntegrationMappingIntegrationTypeField>))]
        public StringEnum<IntegrationMappingIntegrationTypeField> IntegrationType { get; set; }

        /// <summary>
        /// Identifies whether the mapping has
        /// been manually set
        /// (as opposed to being automatically created).
        /// </summary>
        [JsonPropertyName("is_manually_created")]
        public bool? IsManuallyCreated { get; set; }

        [JsonPropertyName("options")]
        public IntegrationMappingSlackOptions Options { get; set; }

        /// <summary>
        /// An object representing the user who
        /// created the integration mapping.
        /// </summary>
        [JsonPropertyName("created_by")]
        public UserIntegrationMappings CreatedBy { get; set; }

        /// <summary>
        /// The user who
        /// last modified the integration mapping.
        /// </summary>
        [JsonPropertyName("modified_by")]
        public UserIntegrationMappings ModifiedBy { get; set; }

        /// <summary>
        /// Mapped item object for Slack.
        /// </summary>
        [JsonPropertyName("partner_item")]
        public IntegrationMappingPartnerItemSlackUnion PartnerItem { get; set; }

        /// <summary>
        /// The Box folder, to which the object from the
        /// partner app domain (referenced in `partner_item_id`) is mapped.
        /// </summary>
        [JsonPropertyName("box_item")]
        public FolderMini BoxItem { get; set; }

        /// <summary>
        /// When the integration mapping object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// When the integration mapping object was last modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; set; }

        public IntegrationMapping(string id, IntegrationMappingPartnerItemSlackUnion partnerItem, FolderMini boxItem, IntegrationMappingBaseTypeField type = IntegrationMappingBaseTypeField.IntegrationMapping) : base(id, type) {
            PartnerItem = partnerItem;
            BoxItem = boxItem;
        }
        
        [JsonConstructorAttribute]
        internal IntegrationMapping(string id, IntegrationMappingPartnerItemSlackUnion partnerItem, FolderMini boxItem, StringEnum<IntegrationMappingBaseTypeField> type) : base(id, type ?? new StringEnum<IntegrationMappingBaseTypeField>(IntegrationMappingBaseTypeField.IntegrationMapping)) {
            PartnerItem = partnerItem;
            BoxItem = boxItem;
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