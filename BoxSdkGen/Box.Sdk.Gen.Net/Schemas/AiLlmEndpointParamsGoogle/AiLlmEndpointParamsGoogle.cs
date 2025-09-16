using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiLlmEndpointParamsGoogle : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_istemperatureSet")]
        protected bool _isTemperatureSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_istop_pSet")]
        protected bool _isTopPSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_istop_kSet")]
        protected bool _isTopKSet { get; set; }

        protected double? _temperature { get; set; }

        protected double? _topP { get; set; }

        protected double? _topK { get; set; }

        /// <summary>
        /// The type of the AI LLM endpoint params object for Google.
        /// This parameter is **required**.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiLlmEndpointParamsGoogleTypeField>))]
        public StringEnum<AiLlmEndpointParamsGoogleTypeField> Type { get; }

        /// <summary>
        /// The temperature is used for sampling during response generation, which occurs when `top-P` and `top-K` are applied. Temperature controls the degree of randomness in the token selection.
        /// </summary>
        [JsonPropertyName("temperature")]
        public double? Temperature { get => _temperature; init { _temperature = value; _isTemperatureSet = true; } }

        /// <summary>
        /// `Top-P` changes how the model selects tokens for output. Tokens are selected from the most (see `top-K`) to least probable until the sum of their probabilities equals the `top-P` value.
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get => _topP; init { _topP = value; _isTopPSet = true; } }

        /// <summary>
        /// `Top-K` changes how the model selects tokens for output. A low `top-K` means the next selected token is
        /// the most probable among all tokens in the model's vocabulary (also called greedy decoding),
        /// while a high `top-K` means that the next token is selected from among the three most probable tokens by using temperature.
        /// </summary>
        [JsonPropertyName("top_k")]
        public double? TopK { get => _topK; init { _topK = value; _isTopKSet = true; } }

        public AiLlmEndpointParamsGoogle(AiLlmEndpointParamsGoogleTypeField type = AiLlmEndpointParamsGoogleTypeField.GoogleParams) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiLlmEndpointParamsGoogle(StringEnum<AiLlmEndpointParamsGoogleTypeField> type) {
            Type = AiLlmEndpointParamsGoogleTypeField.GoogleParams;
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