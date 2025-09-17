using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenBatchBaseV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier that represents a Box Doc Gen batch.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `docgen_batch`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<DocGenBatchBaseV2025R0TypeField>))]
        public StringEnum<DocGenBatchBaseV2025R0TypeField> Type { get; }

        public DocGenBatchBaseV2025R0(string id, DocGenBatchBaseV2025R0TypeField type = DocGenBatchBaseV2025R0TypeField.DocgenBatch) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal DocGenBatchBaseV2025R0(string id, StringEnum<DocGenBatchBaseV2025R0TypeField> type) {
            Id = id;
            Type = DocGenBatchBaseV2025R0TypeField.DocgenBatch;
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