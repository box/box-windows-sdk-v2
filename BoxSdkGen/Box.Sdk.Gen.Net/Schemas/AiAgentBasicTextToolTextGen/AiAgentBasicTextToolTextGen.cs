using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentBasicTextToolTextGen : AiAgentBasicTextToolBase, ISerializable {
        /// <summary>
        /// System messages aim at helping the LLM understand its role and what it is supposed to do.
        /// The input for `{current_date}` is optional, depending on the use.
        /// </summary>
        [JsonPropertyName("system_message")]
        public string? SystemMessage { get; init; }

        /// <summary>
        /// The prompt template contains contextual information of the request and the user prompt.
        /// 
        /// When using the `prompt_template` parameter, you **must include** input for `{user_question}`.
        /// Inputs for `{current_date}` and `{content}` are optional, depending on the use.
        /// </summary>
        [JsonPropertyName("prompt_template")]
        public string? PromptTemplate { get; init; }

        public AiAgentBasicTextToolTextGen() {
            
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