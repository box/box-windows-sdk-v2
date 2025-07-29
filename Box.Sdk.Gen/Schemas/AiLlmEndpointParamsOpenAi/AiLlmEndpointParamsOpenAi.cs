using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiLlmEndpointParamsOpenAi : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_istemperatureSet")]
        protected bool _isTemperatureSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_istop_pSet")]
        protected bool _isTopPSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isfrequency_penaltySet")]
        protected bool _isFrequencyPenaltySet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ispresence_penaltySet")]
        protected bool _isPresencePenaltySet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isstopSet")]
        protected bool _isStopSet { get; set; }

        protected double? _temperature { get; set; }

        protected double? _topP { get; set; }

        protected double? _frequencyPenalty { get; set; }

        protected double? _presencePenalty { get; set; }

        protected string? _stop { get; set; }

        /// <summary>
        /// The type of the AI LLM endpoint params object for OpenAI.
        /// This parameter is **required**.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiLlmEndpointParamsOpenAiTypeField>))]
        public StringEnum<AiLlmEndpointParamsOpenAiTypeField> Type { get; }

        /// <summary>
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, 
        /// while lower values like 0.2 will make it more focused and deterministic. 
        /// We generally recommend altering this or `top_p` but not both.
        /// </summary>
        [JsonPropertyName("temperature")]
        public double? Temperature { get => _temperature; init { _temperature = value; _isTemperatureSet = true; } }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results 
        /// of the tokens with `top_p` probability mass. So 0.1 means only the tokens comprising the top 10% probability 
        /// mass are considered. We generally recommend altering this or temperature but not both.
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get => _topP; init { _topP = value; _isTopPSet = true; } }

        /// <summary>
        /// A number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the 
        /// text so far, decreasing the model's likelihood to repeat the same line verbatim.
        /// </summary>
        [JsonPropertyName("frequency_penalty")]
        public double? FrequencyPenalty { get => _frequencyPenalty; init { _frequencyPenalty = value; _isFrequencyPenaltySet = true; } }

        /// <summary>
        /// A number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far, increasing the model's likelihood to talk about new topics.
        /// </summary>
        [JsonPropertyName("presence_penalty")]
        public double? PresencePenalty { get => _presencePenalty; init { _presencePenalty = value; _isPresencePenaltySet = true; } }

        /// <summary>
        /// Up to 4 sequences where the API will stop generating further tokens.
        /// </summary>
        [JsonPropertyName("stop")]
        public string? Stop { get => _stop; init { _stop = value; _isStopSet = true; } }

        public AiLlmEndpointParamsOpenAi(AiLlmEndpointParamsOpenAiTypeField type = AiLlmEndpointParamsOpenAiTypeField.OpenaiParams) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiLlmEndpointParamsOpenAi(StringEnum<AiLlmEndpointParamsOpenAiTypeField> type) {
            Type = AiLlmEndpointParamsOpenAiTypeField.OpenaiParams;
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