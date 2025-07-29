using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingTeamsCreateRequest : ISerializable {
        [JsonPropertyName("partner_item")]
        public IntegrationMappingPartnerItemTeamsCreateRequest PartnerItem { get; }

        [JsonPropertyName("box_item")]
        public FolderReference BoxItem { get; }

        public IntegrationMappingTeamsCreateRequest(IntegrationMappingPartnerItemTeamsCreateRequest partnerItem, FolderReference boxItem) {
            PartnerItem = partnerItem;
            BoxItem = boxItem;
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