using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAskAgentConverter))]
    public class AiAskAgent {
        internal OneOf<AiAgentReference, AiAgentAsk> _oneOf;
        
        public AiAgentReference AiAgentReference => _oneOf._val0;
        
        public AiAgentAsk AiAgentAsk => _oneOf._val1;
        
        public AiAskAgent(AiAgentReference value) {_oneOf = new OneOf<AiAgentReference, AiAgentAsk>(value);}
        
        public AiAskAgent(AiAgentAsk value) {_oneOf = new OneOf<AiAgentReference, AiAgentAsk>(value);}
        
        public static implicit operator AiAskAgent(AiAgentReference value) => new AiAskAgent(value);
        
        public static implicit operator AiAskAgent(AiAgentAsk value) => new AiAskAgent(value);
        
        class AiAskAgentConverter : JsonConverter<AiAskAgent> {
            public override AiAskAgent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "ai_agent_id":
                                return JsonSerializer.Deserialize<AiAgentReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentReference");
                            case "ai_agent_ask":
                                return JsonSerializer.Deserialize<AiAgentAsk>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentAsk");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, AiAskAgent value, JsonSerializerOptions options) {
                if (value?.AiAgentReference != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentReference, options);
                    return;
                }
                if (value?.AiAgentAsk != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentAsk, options);
                    return;
                }
            }

        }

    }
}