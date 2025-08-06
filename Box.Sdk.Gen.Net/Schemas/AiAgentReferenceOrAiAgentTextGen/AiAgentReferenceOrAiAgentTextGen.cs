using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAgentReferenceOrAiAgentTextGenConverter))]
    public class AiAgentReferenceOrAiAgentTextGen : OneOf<AiAgentReference, AiAgentTextGen> {
        public AiAgentReference? AiAgentReference => _val0;
        
        public AiAgentTextGen? AiAgentTextGen => _val1;
        
        public AiAgentReferenceOrAiAgentTextGen(AiAgentReference value) : base(value) {}
        
        public AiAgentReferenceOrAiAgentTextGen(AiAgentTextGen value) : base(value) {}
        
        public static implicit operator AiAgentReferenceOrAiAgentTextGen(AiAgentReference value) => new AiAgentReferenceOrAiAgentTextGen(value);
        
        public static implicit operator AiAgentReferenceOrAiAgentTextGen(AiAgentTextGen value) => new AiAgentReferenceOrAiAgentTextGen(value);
        
        class AiAgentReferenceOrAiAgentTextGenConverter : JsonConverter<AiAgentReferenceOrAiAgentTextGen> {
            public override AiAgentReferenceOrAiAgentTextGen Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
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

            public override void Write(Utf8JsonWriter writer, AiAgentReferenceOrAiAgentTextGen? value, JsonSerializerOptions options) {
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