using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubSectionTitleTextBlockV2025R0 : HubDocumentBlockV2025R0, ISerializable {
        /// <summary>
        /// The type of this block. The value is always `section_title`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubSectionTitleTextBlockV2025R0TypeField>))]
        public StringEnum<HubSectionTitleTextBlockV2025R0TypeField> Type { get; set; }

        /// <summary>
        /// Text content of the block. Includes rich text formatting.
        /// </summary>
        [JsonPropertyName("fragment")]
        public string Fragment { get; set; }

        public HubSectionTitleTextBlockV2025R0(string id, string fragment, HubSectionTitleTextBlockV2025R0TypeField type = HubSectionTitleTextBlockV2025R0TypeField.SectionTitle) : base(id) {
            Type = type;
            Fragment = fragment;
        }
        
        [JsonConstructorAttribute]
        internal HubSectionTitleTextBlockV2025R0(string id, string fragment, StringEnum<HubSectionTitleTextBlockV2025R0TypeField> type) : base(id) {
            Type = HubSectionTitleTextBlockV2025R0TypeField.SectionTitle;
            Fragment = fragment;
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