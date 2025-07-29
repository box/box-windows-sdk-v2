using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiCitation : ISerializable {
        /// <summary>
        /// The specific content from where the answer was referenced.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; init; }

        /// <summary>
        /// The id of the item.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The type of the item.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiCitationTypeField>))]
        public StringEnum<AiCitationTypeField>? Type { get; init; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        public AiCitation() {
            
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