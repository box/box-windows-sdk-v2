using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAgentConverter))]
    public class AiAgent {
        internal OneOf<AiAgentAsk, AiAgentTextGen, AiAgentExtract, AiAgentExtractStructured> _oneOf;
        
        public AiAgentAsk? AiAgentAsk => _oneOf._val0;
        
        public AiAgentTextGen? AiAgentTextGen => _oneOf._val1;
        
        public AiAgentExtract? AiAgentExtract => _oneOf._val2;
        
        public AiAgentExtractStructured? AiAgentExtractStructured => _oneOf._val3;
        
        public AiAgent(AiAgentAsk value) {_oneOf = new OneOf<AiAgentAsk, AiAgentTextGen, AiAgentExtract, AiAgentExtractStructured>(value);}
        
        public AiAgent(AiAgentTextGen value) {_oneOf = new OneOf<AiAgentAsk, AiAgentTextGen, AiAgentExtract, AiAgentExtractStructured>(value);}
        
        public AiAgent(AiAgentExtract value) {_oneOf = new OneOf<AiAgentAsk, AiAgentTextGen, AiAgentExtract, AiAgentExtractStructured>(value);}
        
        public AiAgent(AiAgentExtractStructured value) {_oneOf = new OneOf<AiAgentAsk, AiAgentTextGen, AiAgentExtract, AiAgentExtractStructured>(value);}
        
        public static implicit operator AiAgent(AiAgentAsk value) => new AiAgent(value);
        
        public static implicit operator AiAgent(AiAgentTextGen value) => new AiAgent(value);
        
        public static implicit operator AiAgent(AiAgentExtract value) => new AiAgent(value);
        
        public static implicit operator AiAgent(AiAgentExtractStructured value) => new AiAgent(value);
        
        class AiAgentConverter : JsonConverter<AiAgent> {
            public override AiAgent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "ai_agent_ask":
                            return JsonSerializer.Deserialize<AiAgentAsk>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentAsk");
                        case "ai_agent_text_gen":
                            return JsonSerializer.Deserialize<AiAgentTextGen>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentTextGen");
                        case "ai_agent_extract":
                            return JsonSerializer.Deserialize<AiAgentExtract>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtract");
                        case "ai_agent_extract_structured":
                            return JsonSerializer.Deserialize<AiAgentExtractStructured>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtractStructured");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiAgent? value, JsonSerializerOptions options) {
                if (value?.AiAgentAsk != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentAsk, options);
                    return;
                }
                if (value?.AiAgentTextGen != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentTextGen, options);
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
            }

        }

    }
}