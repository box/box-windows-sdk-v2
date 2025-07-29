using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenJobBaseV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier that represent a Box Doc Gen job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `docgen_job`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<DocGenJobBaseV2025R0TypeField>))]
        public StringEnum<DocGenJobBaseV2025R0TypeField> Type { get; }

        public DocGenJobBaseV2025R0(string id, DocGenJobBaseV2025R0TypeField type = DocGenJobBaseV2025R0TypeField.DocgenJob) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal DocGenJobBaseV2025R0(string id, StringEnum<DocGenJobBaseV2025R0TypeField> type) {
            Id = id;
            Type = DocGenJobBaseV2025R0TypeField.DocgenJob;
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