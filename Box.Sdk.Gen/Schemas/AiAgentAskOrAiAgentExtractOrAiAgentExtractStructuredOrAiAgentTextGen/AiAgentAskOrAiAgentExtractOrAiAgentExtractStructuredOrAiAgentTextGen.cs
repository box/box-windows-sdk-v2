using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGenConverter))]
    public class AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen : OneOf<AiAgentAsk, AiAgentExtract, AiAgentExtractStructured, AiAgentTextGen> {
        public AiAgentAsk? AiAgentAsk => _val0;
        
        public AiAgentExtract? AiAgentExtract => _val1;
        
        public AiAgentExtractStructured? AiAgentExtractStructured => _val2;
        
        public AiAgentTextGen? AiAgentTextGen => _val3;
        
        public AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentAsk value) : base(value) {}
        
        public AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentExtract value) : base(value) {}
        
        public AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentExtractStructured value) : base(value) {}
        
        public AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentTextGen value) : base(value) {}
        
        public static implicit operator AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentAsk value) => new AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(value);
        
        public static implicit operator AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentExtract value) => new AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(value);
        
        public static implicit operator AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentExtractStructured value) => new AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(value);
        
        public static implicit operator AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(AiAgentTextGen value) => new AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen(value);
        
        class AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGenConverter : JsonConverter<AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen> {
            public override AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "ai_agent_ask":
                            return JsonSerializer.Deserialize<AiAgentAsk>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentAsk");
                        case "ai_agent_extract":
                            return JsonSerializer.Deserialize<AiAgentExtract>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtract");
                        case "ai_agent_extract_structured":
                            return JsonSerializer.Deserialize<AiAgentExtractStructured>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtractStructured");
                        case "ai_agent_text_gen":
                            return JsonSerializer.Deserialize<AiAgentTextGen>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentTextGen");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen? value, JsonSerializerOptions options) {
                if (value?.AiAgentAsk != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentAsk, options);
                    return;
                }
                if (value?.AiAgentExtract != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentExtract, options);
                    return;
                }
                if (value?.AiAgentExtractStructured != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentExtractStructured, options);
                    return;
                }
                if (value?.AiAgentTextGen != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentTextGen, options);
                    return;
                }
            }

        }

    }
}