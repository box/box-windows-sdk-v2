using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiExtractAgentConverter))]
    public class AiExtractAgent {
        internal OneOf<AiAgentReference, AiAgentExtract> _oneOf;
        
        public AiAgentReference? AiAgentReference => _oneOf._val0;
        
        public AiAgentExtract? AiAgentExtract => _oneOf._val1;
        
        public AiExtractAgent(AiAgentReference value) {_oneOf = new OneOf<AiAgentReference, AiAgentExtract>(value);}
        
        public AiExtractAgent(AiAgentExtract value) {_oneOf = new OneOf<AiAgentReference, AiAgentExtract>(value);}
        
        public static implicit operator AiExtractAgent(AiAgentReference value) => new AiExtractAgent(value);
        
        public static implicit operator AiExtractAgent(AiAgentExtract value) => new AiExtractAgent(value);
        
        class AiExtractAgentConverter : JsonConverter<AiExtractAgent> {
            public override AiExtractAgent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "ai_agent_id":
                            return JsonSerializer.Deserialize<AiAgentReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentReference");
                        case "ai_agent_extract":
                            return JsonSerializer.Deserialize<AiAgentExtract>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtract");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiExtractAgent? value, JsonSerializerOptions options) {
                if (value?.AiAgentReference != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentReference, options);
                    return;
                }
                if (value?.AiAgentExtract != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentExtract, options);
                    return;
                }
            }

        }

    }
}