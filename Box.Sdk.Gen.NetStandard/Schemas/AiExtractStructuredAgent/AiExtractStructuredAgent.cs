using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiExtractStructuredAgentConverter))]
    public class AiExtractStructuredAgent {
        internal OneOf<AiAgentReference, AiAgentExtractStructured> _oneOf;
        
        public AiAgentReference AiAgentReference => _oneOf._val0;
        
        public AiAgentExtractStructured AiAgentExtractStructured => _oneOf._val1;
        
        public AiExtractStructuredAgent(AiAgentReference value) {_oneOf = new OneOf<AiAgentReference, AiAgentExtractStructured>(value);}
        
        public AiExtractStructuredAgent(AiAgentExtractStructured value) {_oneOf = new OneOf<AiAgentReference, AiAgentExtractStructured>(value);}
        
        public static implicit operator AiExtractStructuredAgent(AiAgentReference value) => new AiExtractStructuredAgent(value);
        
        public static implicit operator AiExtractStructuredAgent(AiAgentExtractStructured value) => new AiExtractStructuredAgent(value);
        
        class AiExtractStructuredAgentConverter : JsonConverter<AiExtractStructuredAgent> {
            public override AiExtractStructuredAgent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "ai_agent_id":
                                return JsonSerializer.Deserialize<AiAgentReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentReference");
                            case "ai_agent_extract_structured":
                                return JsonSerializer.Deserialize<AiAgentExtractStructured>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtractStructured");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, AiExtractStructuredAgent value, JsonSerializerOptions options) {
                if (value?.AiAgentReference != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentReference, options);
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