using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(GroupMiniOrUserCollaborationsConverter))]
    public class GroupMiniOrUserCollaborations : OneOf<GroupMini, UserCollaborations> {
        public GroupMini? GroupMini => _val0;
        
        public UserCollaborations? UserCollaborations => _val1;
        
        public GroupMiniOrUserCollaborations(GroupMini value) : base(value) {}
        
        public GroupMiniOrUserCollaborations(UserCollaborations value) : base(value) {}
        
        public static implicit operator GroupMiniOrUserCollaborations(GroupMini value) => new GroupMiniOrUserCollaborations(value);
        
        public static implicit operator GroupMiniOrUserCollaborations(UserCollaborations value) => new GroupMiniOrUserCollaborations(value);
        
        class GroupMiniOrUserCollaborationsConverter : JsonConverter<GroupMiniOrUserCollaborations> {
            public override GroupMiniOrUserCollaborations Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "group":
                            return JsonSerializer.Deserialize<GroupMini>(document) ?? throw new Exception($"Could not deserialize {document} to GroupMini");
                        case "user":
                            return JsonSerializer.Deserialize<UserCollaborations>(document) ?? throw new Exception($"Could not deserialize {document} to UserCollaborations");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, GroupMiniOrUserCollaborations? value, JsonSerializerOptions options) {
                if (value?.GroupMini != null) {
                    JsonSerializer.Serialize(writer, value.GroupMini, options);
                    return;
                }
                if (value?.UserCollaborations != null) {
                    JsonSerializer.Serialize(writer, value.UserCollaborations, options);
                    return;
                }
            }

        }

    }
}