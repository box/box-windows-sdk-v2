using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiLlmEndpointParamsConverter))]
    public class AiLlmEndpointParams : OneOf<AiLlmEndpointParamsOpenAi, AiLlmEndpointParamsGoogle, AiLlmEndpointParamsAws, AiLlmEndpointParamsIbm> {
        public AiLlmEndpointParamsOpenAi? AiLlmEndpointParamsOpenAi => _val0;
        
        public AiLlmEndpointParamsGoogle? AiLlmEndpointParamsGoogle => _val1;
        
        public AiLlmEndpointParamsAws? AiLlmEndpointParamsAws => _val2;
        
        public AiLlmEndpointParamsIbm? AiLlmEndpointParamsIbm => _val3;
        
        public AiLlmEndpointParams(AiLlmEndpointParamsOpenAi value) : base(value) {}
        
        public AiLlmEndpointParams(AiLlmEndpointParamsGoogle value) : base(value) {}
        
        public AiLlmEndpointParams(AiLlmEndpointParamsAws value) : base(value) {}
        
        public AiLlmEndpointParams(AiLlmEndpointParamsIbm value) : base(value) {}
        
        public static implicit operator AiLlmEndpointParams(AiLlmEndpointParamsOpenAi value) => new AiLlmEndpointParams(value);
        
        public static implicit operator AiLlmEndpointParams(AiLlmEndpointParamsGoogle value) => new AiLlmEndpointParams(value);
        
        public static implicit operator AiLlmEndpointParams(AiLlmEndpointParamsAws value) => new AiLlmEndpointParams(value);
        
        public static implicit operator AiLlmEndpointParams(AiLlmEndpointParamsIbm value) => new AiLlmEndpointParams(value);
        
        class AiLlmEndpointParamsConverter : JsonConverter<AiLlmEndpointParams> {
            public override AiLlmEndpointParams Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "openai_params":
                            return JsonSerializer.Deserialize<AiLlmEndpointParamsOpenAi>(document) ?? throw new Exception($"Could not deserialize {document} to AiLlmEndpointParamsOpenAi");
                        case "google_params":
                            return JsonSerializer.Deserialize<AiLlmEndpointParamsGoogle>(document) ?? throw new Exception($"Could not deserialize {document} to AiLlmEndpointParamsGoogle");
                        case "aws_params":
                            return JsonSerializer.Deserialize<AiLlmEndpointParamsAws>(document) ?? throw new Exception($"Could not deserialize {document} to AiLlmEndpointParamsAws");
                        case "ibm_params":
                            return JsonSerializer.Deserialize<AiLlmEndpointParamsIbm>(document) ?? throw new Exception($"Could not deserialize {document} to AiLlmEndpointParamsIbm");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiLlmEndpointParams? value, JsonSerializerOptions options) {
                if (value?.AiLlmEndpointParamsOpenAi != null) {
                    JsonSerializer.Serialize(writer, value.AiLlmEndpointParamsOpenAi, options);
                    return;
                }
                if (value?.AiLlmEndpointParamsGoogle != null) {
                    JsonSerializer.Serialize(writer, value.AiLlmEndpointParamsGoogle, options);
                    return;
                }
                if (value?.AiLlmEndpointParamsAws != null) {
                    JsonSerializer.Serialize(writer, value.AiLlmEndpointParamsAws, options);
                    return;
                }
                if (value?.AiLlmEndpointParamsIbm != null) {
                    JsonSerializer.Serialize(writer, value.AiLlmEndpointParamsIbm, options);
                    return;
                }
            }

        }

    }
}