using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleRunV2026R0StatsField : ISerializable {
        /// <summary>
        /// Bytes transferred during the run.
        /// </summary>
        [JsonPropertyName("bytes_sent")]
        public long? BytesSent { get; set; }

        /// <summary>
        /// Files transferred during the run.
        /// </summary>
        [JsonPropertyName("files_sent")]
        public long? FilesSent { get; set; }

        /// <summary>
        /// Folders created on the destination during the run.
        /// </summary>
        [JsonPropertyName("folders_created")]
        public long? FoldersCreated { get; set; }

        /// <summary>
        /// Whether these figures are estimates rather than final counts.
        /// </summary>
        [JsonPropertyName("is_estimated")]
        public bool? IsEstimated { get; set; }

        public ShuttleRunV2026R0StatsField() {
            
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