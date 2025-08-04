using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingPartnerItemSlack : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isslack_workspace_idSet")]
        protected bool _isSlackWorkspaceIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isslack_org_idSet")]
        protected bool _isSlackOrgIdSet { get; set; }

        protected string? _slackWorkspaceId { get; set; }

        protected string? _slackOrgId { get; set; }

        /// <summary>
        /// Type of the mapped item referenced in `id`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<IntegrationMappingPartnerItemSlackTypeField>))]
        public StringEnum<IntegrationMappingPartnerItemSlackTypeField> Type { get; }

        /// <summary>
        /// ID of the mapped item (of type referenced in `type`).
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// ID of the Slack workspace with which the item is associated. Use this parameter if Box for Slack is installed at a workspace level. Do not use `slack_org_id` at the same time.
        /// </summary>
        [JsonPropertyName("slack_workspace_id")]
        public string? SlackWorkspaceId { get => _slackWorkspaceId; init { _slackWorkspaceId = value; _isSlackWorkspaceIdSet = true; } }

        /// <summary>
        /// ID of the Slack org with which the item is associated. Use this parameter if Box for Slack is installed at the org level. Do not use `slack_workspace_id` at the same time.
        /// </summary>
        [JsonPropertyName("slack_org_id")]
        public string? SlackOrgId { get => _slackOrgId; init { _slackOrgId = value; _isSlackOrgIdSet = true; } }

        public IntegrationMappingPartnerItemSlack(string id, IntegrationMappingPartnerItemSlackTypeField type = IntegrationMappingPartnerItemSlackTypeField.Channel) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal IntegrationMappingPartnerItemSlack(string id, StringEnum<IntegrationMappingPartnerItemSlackTypeField> type) {
            Type = IntegrationMappingPartnerItemSlackTypeField.Channel;
            Id = id;
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