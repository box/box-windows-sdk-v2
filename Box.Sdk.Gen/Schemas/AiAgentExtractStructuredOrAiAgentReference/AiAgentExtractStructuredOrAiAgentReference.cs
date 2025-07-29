using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAgentExtractStructuredOrAiAgentReferenceConverter))]
    public class AiAgentExtractStructuredOrAiAgentReference : OneOf<AiAgentExtractStructured, AiAgentReference> {
        public AiAgentExtractStructured? AiAgentExtractStructured => _val0;
        
        public AiAgentReference? AiAgentReference => _val1;
        
        public AiAgentExtractStructuredOrAiAgentReference(AiAgentExtractStructured value) : base(value) {}
        
        public AiAgentExtractStructuredOrAiAgentReference(AiAgentReference value) : base(value) {}
        
        public static implicit operator AiAgentExtractStructuredOrAiAgentReference(AiAgentExtractStructured value) => new AiAgentExtractStructuredOrAiAgentReference(value);
        
        public static implicit operator AiAgentExtractStructuredOrAiAgentReference(AiAgentReference value) => new AiAgentExtractStructuredOrAiAgentReference(value);
        
        class AiAgentExtractStructuredOrAiAgentReferenceConverter : JsonConverter<AiAgentExtractStructuredOrAiAgentReference> {
            public override AiAgentExtractStructuredOrAiAgentReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "ai_agent_extract_structured":
                            return JsonSerializer.Deserialize<AiAgentExtractStructured>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtractStructured");
                        case "ai_agent_id":
                            return JsonSerializer.Deserialize<AiAgentReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentReference");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiAgentExtractStructuredOrAiAgentReference? value, JsonSerializerOptions options) {
                if (value?.AiAgentExtractStructured != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentExtractStructured, options);
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