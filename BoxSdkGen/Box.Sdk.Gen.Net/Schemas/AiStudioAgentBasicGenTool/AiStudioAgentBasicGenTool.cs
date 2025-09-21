using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiStudioAgentBasicGenTool : AiAgentBasicGenTool, ISerializable {
        /// <summary>
        /// True if system message contains custom instructions placeholder, false otherwise.
        /// </summary>
        [JsonPropertyName("is_custom_instructions_included")]
        public bool? IsCustomInstructionsIncluded { get; init; }

        public AiStudioAgentBasicGenTool() {
            
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