using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiItemAsk : ISerializable {
        /// <summary>
        /// The ID of the file.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of the item. A `hubs` item must be used as a single item.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiItemAskTypeField>))]
        public StringEnum<AiItemAskTypeField> Type { get; }

        /// <summary>
        /// The content of the item, often the text representation.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; init; }

        public AiItemAsk(string id, AiItemAskTypeField type) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiItemAsk(string id, StringEnum<AiItemAskTypeField> type) {
            Id = id;
            Type = type;
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