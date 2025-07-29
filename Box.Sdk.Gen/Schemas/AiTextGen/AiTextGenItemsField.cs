using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiTextGenItemsField : ISerializable {
        /// <summary>
        /// The ID of the item.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of the item.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiTextGenItemsTypeField>))]
        public StringEnum<AiTextGenItemsTypeField> Type { get; }

        /// <summary>
        /// The content to use as context for generating new text or editing existing text.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; init; }

        public AiTextGenItemsField(string id, AiTextGenItemsTypeField type = AiTextGenItemsTypeField.File) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiTextGenItemsField(string id, StringEnum<AiTextGenItemsTypeField> type) {
            Id = id;
            Type = AiTextGenItemsTypeField.File;
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