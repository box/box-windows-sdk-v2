using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiTextGenAgentConverter))]
    public class AiTextGenAgent {
        internal OneOf<AiAgentReference, AiAgentTextGen> _oneOf;
        
        public AiAgentReference AiAgentReference => _oneOf._val0;
        
        public AiAgentTextGen AiAgentTextGen => _oneOf._val1;
        
        public AiTextGenAgent(AiAgentReference value) {_oneOf = new OneOf<AiAgentReference, AiAgentTextGen>(value);}
        
        public AiTextGenAgent(AiAgentTextGen value) {_oneOf = new OneOf<AiAgentReference, AiAgentTextGen>(value);}
        
        public static implicit operator AiTextGenAgent(AiAgentReference value) => new AiTextGenAgent(value);
        
        public static implicit operator AiTextGenAgent(AiAgentTextGen value) => new AiTextGenAgent(value);
        
        class AiTextGenAgentConverter : JsonConverter<AiTextGenAgent> {
            public override AiTextGenAgent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "ai_agent_id":
                                return JsonSerializer.Deserialize<AiAgentReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentReference");
                            case "ai_agent_text_gen":
                                return JsonSerializer.Deserialize<AiAgentTextGen>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentTextGen");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, AiTextGenAgent value, JsonSerializerOptions options) {
                if (value?.AiAgentReference != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentReference, options);
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