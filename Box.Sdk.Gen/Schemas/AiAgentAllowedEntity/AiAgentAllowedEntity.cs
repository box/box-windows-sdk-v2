using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiAgentAllowedEntityConverter))]
    public class AiAgentAllowedEntity : OneOf<UserBase, GroupBase> {
        public UserBase? UserBase => _val0;
        
        public GroupBase? GroupBase => _val1;
        
        public AiAgentAllowedEntity(UserBase value) : base(value) {}
        
        public AiAgentAllowedEntity(GroupBase value) : base(value) {}
        
        public static implicit operator AiAgentAllowedEntity(UserBase value) => new AiAgentAllowedEntity(value);
        
        public static implicit operator AiAgentAllowedEntity(GroupBase value) => new AiAgentAllowedEntity(value);
        
        class AiAgentAllowedEntityConverter : JsonConverter<AiAgentAllowedEntity> {
            public override AiAgentAllowedEntity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "user":
                            return JsonSerializer.Deserialize<UserBase>(document) ?? throw new Exception($"Could not deserialize {document} to UserBase");
                        case "group":
                            return JsonSerializer.Deserialize<GroupBase>(document) ?? throw new Exception($"Could not deserialize {document} to GroupBase");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AiAgentAllowedEntity? value, JsonSerializerOptions options) {
                if (value?.UserBase != null) {
                    JsonSerializer.Serialize(writer, value.UserBase, options);
                    return;
                }
                if (value?.GroupBase != null) {
                    JsonSerializer.Serialize(writer, value.GroupBase, options);
                    return;
                }
            }

        }

    }
}