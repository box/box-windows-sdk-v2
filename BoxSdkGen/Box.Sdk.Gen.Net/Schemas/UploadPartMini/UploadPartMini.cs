using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class UploadPartMini : ISerializable {
        /// <summary>
        /// The unique ID of the chunk.
        /// </summary>
        [JsonPropertyName("part_id")]
        public string? PartId { get; init; }

        /// <summary>
        /// The offset of the chunk within the file
        /// in bytes. The lower bound of the position
        /// of the chunk within the file.
        /// </summary>
        [JsonPropertyName("offset")]
        public long? Offset { get; init; }

        /// <summary>
        /// The size of the chunk in bytes.
        /// </summary>
        [JsonPropertyName("size")]
        public long? Size { get; init; }

        public UploadPartMini() {
            
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