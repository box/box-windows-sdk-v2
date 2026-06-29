using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiItemAsk : ISerializable {
        /// <summary>
        /// The ID of the file, or the ID of the Box Hub when `type` is `hubs`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of the item. Use `file` to ask a question about a file, or `hubs` to
        /// search across and ask a question about the entire contents of a Box Hub.
        /// A `hubs` item must be the only item in the request.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiItemAskTypeField>))]
        public StringEnum<AiItemAskTypeField> Type { get; set; }

        /// <summary>
        /// The content of the item, often the text representation.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        public AiItemAsk(string id, AiItemAskTypeField type) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiItemAsk(string id, StringEnum<AiItemAskTypeField> type) {
            Id = id;
            Type = type;
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