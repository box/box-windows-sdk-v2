using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiLlmEndpointParamsAws : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_istemperatureSet")]
        protected bool _isTemperatureSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_istop_pSet")]
        protected bool _isTopPSet { get; set; }

        protected double? _temperature { get; set; }

        protected double? _topP { get; set; }

        /// <summary>
        /// The type of the AI LLM endpoint params object for AWS.
        /// This parameter is **required**.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiLlmEndpointParamsAwsTypeField>))]
        public StringEnum<AiLlmEndpointParamsAwsTypeField> Type { get; }

        /// <summary>
        /// What sampling temperature to use, between 0 and 1. Higher values like 0.8 will make the output more random, 
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

        public AiLlmEndpointParamsAws(AiLlmEndpointParamsAwsTypeField type = AiLlmEndpointParamsAwsTypeField.AwsParams) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiLlmEndpointParamsAws(StringEnum<AiLlmEndpointParamsAwsTypeField> type) {
            Type = AiLlmEndpointParamsAwsTypeField.AwsParams;
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