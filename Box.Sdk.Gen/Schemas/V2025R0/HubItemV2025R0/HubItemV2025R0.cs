using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class HubItemV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier for this item.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of the item.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubItemV2025R0TypeField>))]
        public StringEnum<HubItemV2025R0TypeField> Type { get; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        public HubItemV2025R0(string id, HubItemV2025R0TypeField type, string name) {
            Id = id;
            Type = type;
            Name = name;
        }
        
        [JsonConstructorAttribute]
        internal HubItemV2025R0(string id, StringEnum<HubItemV2025R0TypeField> type, string name) {
            Id = id;
            Type = type;
            Name = name;
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