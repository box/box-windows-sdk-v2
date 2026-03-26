using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubDividerBlockV2025R0 : HubDocumentBlockV2025R0, ISerializable {
        /// <summary>
        /// The type of this block. The value is always `divider`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubDividerBlockV2025R0TypeField>))]
        public StringEnum<HubDividerBlockV2025R0TypeField> Type { get; set; }

        public HubDividerBlockV2025R0(string id, HubDividerBlockV2025R0TypeField type = HubDividerBlockV2025R0TypeField.Divider) : base(id) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal HubDividerBlockV2025R0(string id, StringEnum<HubDividerBlockV2025R0TypeField> type) : base(id) {
            Type = HubDividerBlockV2025R0TypeField.Divider;
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