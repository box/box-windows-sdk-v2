using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentBasicGenTool : AiAgentLongTextToolTextGen, ISerializable {
        /// <summary>
        /// How the content should be included in a request to the LLM.
        /// Input for `{content}` is optional, depending on the use.
        /// </summary>
        [JsonPropertyName("content_template")]
        public string? ContentTemplate { get; init; }

        public AiAgentBasicGenTool() {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}