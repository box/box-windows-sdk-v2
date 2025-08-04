using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAgentExtractOrAiAgentReferenceConverter))]
    public class AiAgentExtractOrAiAgentReference : OneOf<AiAgentExtract, AiAgentReference> {
        public AiAgentExtract? AiAgentExtract => _val0;
        
        public AiAgentReference? AiAgentReference => _val1;
        
        public AiAgentExtractOrAiAgentReference(AiAgentExtract value) : base(value) {}
        
        public AiAgentExtractOrAiAgentReference(AiAgentReference value) : base(value) {}
        
        public static implicit operator AiAgentExtractOrAiAgentReference(AiAgentExtract value) => new AiAgentExtractOrAiAgentReference(value);
        
        public static implicit operator AiAgentExtractOrAiAgentReference(AiAgentReference value) => new AiAgentExtractOrAiAgentReference(value);
        
        class AiAgentExtractOrAiAgentReferenceConverter : JsonConverter<AiAgentExtractOrAiAgentReference> {
            public override AiAgentExtractOrAiAgentReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "ai_agent_extract":
                            return JsonSerializer.Deserialize<AiAgentExtract>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentExtract");
                        case "ai_agent_id":
                            return JsonSerializer.Deserialize<AiAgentReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiAgentReference");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiAgentExtractOrAiAgentReference? value, JsonSerializerOptions options) {
                if (value?.AiAgentExtract != null) {
                    JsonSerializer.Serialize(writer, value.AiAgentExtract, options);
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