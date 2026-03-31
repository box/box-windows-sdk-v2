using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubItemListBlockV2025R0 : HubDocumentBlockV2025R0, ISerializable {
        /// <summary>
        /// The type of this block. The value is always `item_list`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubItemListBlockV2025R0TypeField>))]
        public StringEnum<HubItemListBlockV2025R0TypeField> Type { get; }

        public HubItemListBlockV2025R0(string id, HubItemListBlockV2025R0TypeField type = HubItemListBlockV2025R0TypeField.ItemList) : base(id) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal HubItemListBlockV2025R0(string id, StringEnum<HubItemListBlockV2025R0TypeField> type) : base(id) {
            Type = HubItemListBlockV2025R0TypeField.ItemList;
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