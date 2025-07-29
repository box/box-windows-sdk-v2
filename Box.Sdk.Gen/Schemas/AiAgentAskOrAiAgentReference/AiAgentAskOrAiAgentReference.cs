using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAgentAskOrAiAgentReferenceConverter))]
    public class AiAgentAskOrAiAgentReference : OneOf<AiAgentAsk, AiAgentReference> {
        public AiAgentAsk? AiAgentAsk => _val0;
        
        public AiAgentReference? AiAgentReference => _val1;
        
        public AiAgentAskOrAiAgentReference(AiAgentAsk value) : base(value) {}
        
        public AiAgentAskOrAiAgentReference(AiAgentReference value) : base(value) {}
        
        public static implicit operator AiAgentAskOrAiAgentReference(AiAgentAsk value) => new AiAgentAskOrAiAgentReference(value);
        
        public static implicit operator AiAgentAskOrAiAgentReference(AiAgentReference value) => new AiAgentAskOrAiAgentReference(value);
        
        class AiAgentAskOrAiAgentReferenceConverter : JsonConverter<AiAgentAskOrAiAgentReference> {
            public override AiAgentAskOrAiAgentReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "ai_agent_ask":
                            return JsonSerializer.Deserialize<AiAgentAsk>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentAsk");
                        case "ai_agent_id":
                            return JsonSerializer.Deserialize<AiAgentReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentReference");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiAgentAskOrAiAgentReference? value, JsonSerializerOptions options) {
                if (value?.AiAgentAsk != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentAsk, options);
                    return;
                }
                if (value?.AiAgentReference != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentReference, options);
                    return;
                }
            }

        }

    }
}