using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class UploadPartPlanHit : ISerializable {
        /// <summary>
        /// The offset of the chunk within the file
        /// in bytes. The lower bound of the position
        /// of the chunk within the file.
        /// </summary>
        [JsonPropertyName("offset")]
        public long Offset { get; }

        /// <summary>
        /// The size of the chunk in bytes.
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; }

        /// <summary>
        /// The `SHA-512` hash of the chunk.
        /// </summary>
        [JsonPropertyName("sha512")]
        public string Sha512 { get; }

        /// <summary>
        /// The unique ID of the chunk.
        /// </summary>
        [JsonPropertyName("part_id")]
        public string PartId { get; }

        public UploadPartPlanHit(long offset, long size, string sha512, string partId) {
            Offset = offset;
            Size = size;
            Sha512 = sha512;
            PartId = partId;
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