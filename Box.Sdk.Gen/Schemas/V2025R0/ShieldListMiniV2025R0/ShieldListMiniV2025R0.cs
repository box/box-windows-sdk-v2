using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldListMiniV2025R0 : ISerializable {
        /// <summary>
        /// Unique global identifier for this list.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of object.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldListMiniV2025R0TypeField>))]
        public StringEnum<ShieldListMiniV2025R0TypeField> Type { get; }

        /// <summary>
        /// Name of Shield List.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("content")]
        public ShieldListMiniV2025R0ContentField Content { get; }

        public ShieldListMiniV2025R0(string id, string name, ShieldListMiniV2025R0ContentField content, ShieldListMiniV2025R0TypeField type = ShieldListMiniV2025R0TypeField.ShieldList) {
            Id = id;
            Type = type;
            Name = name;
            Content = content;
        }
        
        [JsonConstructorAttribute]
        internal ShieldListMiniV2025R0(string id, string name, ShieldListMiniV2025R0ContentField content, StringEnum<ShieldListMiniV2025R0TypeField> type) {
            Id = id;
            Type = ShieldListMiniV2025R0TypeField.ShieldList;
            Name = name;
            Content = content;
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