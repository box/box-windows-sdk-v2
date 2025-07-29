using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class RealtimeServer : ISerializable {
        /// <summary>
        /// The value will always be `realtime_server`.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        /// <summary>
        /// The URL for the server.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        /// The time in minutes for which this server is available.
        /// </summary>
        [JsonPropertyName("ttl")]
        public string? Ttl { get; init; }

        /// <summary>
        /// The maximum number of retries this server will
        /// allow before a new long poll should be started by
        /// getting a [new list of server](#options-events).
        /// </summary>
        [JsonPropertyName("max_retries")]
        public string? MaxRetries { get; init; }

        /// <summary>
        /// The maximum number of seconds without a response
        /// after which you should retry the long poll connection.
        /// 
        /// This helps to overcome network issues where the long
        /// poll looks to be working but no packages are coming
        /// through.
        /// </summary>
        [JsonPropertyName("retry_timeout")]
        public long? RetryTimeout { get; init; }

        public RealtimeServer() {
            
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