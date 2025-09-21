using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingSlackCreateRequest : ISerializable {
        [JsonPropertyName("partner_item")]
        public IntegrationMappingPartnerItemSlack PartnerItem { get; set; }

        [JsonPropertyName("box_item")]
        public IntegrationMappingBoxItemSlack BoxItem { get; set; }

        [JsonPropertyName("options")]
        public IntegrationMappingSlackOptions Options { get; set; }

        public IntegrationMappingSlackCreateRequest(IntegrationMappingPartnerItemSlack partnerItem, IntegrationMappingBoxItemSlack boxItem) {
            PartnerItem = partnerItem;
            BoxItem = boxItem;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}