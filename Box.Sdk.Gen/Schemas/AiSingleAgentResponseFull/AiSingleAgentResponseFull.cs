using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiSingleAgentResponseFull : AiSingleAgentResponse, ISerializable {
        [JsonPropertyName("ask")]
        public AiStudioAgentAskResponse? Ask { get; init; }

        [JsonPropertyName("text_gen")]
        public AiStudioAgentTextGenResponse? TextGen { get; init; }

        [JsonPropertyName("extract")]
        public AiStudioAgentExtractResponse? Extract { get; init; }

        public AiSingleAgentResponseFull(string id, string origin, string name, string accessState) : base(id, origin, name, accessState) {
            
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