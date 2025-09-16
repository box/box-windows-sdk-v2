using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(CollaborationAccessGranteeConverter))]
    public class CollaborationAccessGrantee {
        internal OneOf<UserCollaborations, GroupMini> _oneOf;
        
        public UserCollaborations UserCollaborations => _oneOf._val0;
        
        public GroupMini GroupMini => _oneOf._val1;
        
        public CollaborationAccessGrantee(UserCollaborations value) {_oneOf = new OneOf<UserCollaborations, GroupMini>(value);}
        
        public CollaborationAccessGrantee(GroupMini value) {_oneOf = new OneOf<UserCollaborations, GroupMini>(value);}
        
        public static implicit operator CollaborationAccessGrantee(UserCollaborations value) => new CollaborationAccessGrantee(value);
        
        public static implicit operator CollaborationAccessGrantee(GroupMini value) => new CollaborationAccessGrantee(value);
        
        class CollaborationAccessGranteeConverter : JsonConverter<CollaborationAccessGrantee> {
            public override CollaborationAccessGrantee Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "user":
                                return JsonSerializer.Deserialize<UserCollaborations>(document) ?? throw new Exception($"Could not deserialize {document} to UserCollaborations");
                            case "group":
                                return JsonSerializer.Deserialize<GroupMini>(document) ?? throw new Exception($"Could not deserialize {document} to GroupMini");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, CollaborationAccessGrantee value, JsonSerializerOptions options) {
                if (value?.UserCollaborations != null) {
                    JsonSerializer.Serialize(writer, value.UserCollaborations, options);
                    return;
                }
                if (value?.GroupMini != null) {
                    JsonSerializer.Serialize(writer, value.GroupMini, options);
                    return;
                }
            }

        }

    }
}