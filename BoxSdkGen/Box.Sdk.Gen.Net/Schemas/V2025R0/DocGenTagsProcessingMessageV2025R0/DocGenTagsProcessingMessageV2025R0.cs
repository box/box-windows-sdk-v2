using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenTagsProcessingMessageV2025R0 : ISerializable {
        /// <summary>
        /// A message informing the user that document tags are still being processed.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; }

        public DocGenTagsProcessingMessageV2025R0(string message) {
            Message = message;
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