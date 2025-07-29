using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(HubAccessGranteeV2025R0Converter))]
    public class HubAccessGranteeV2025R0 : OneOf<HubCollaborationUserV2025R0, GroupMiniV2025R0> {
        public HubCollaborationUserV2025R0? HubCollaborationUserV2025R0 => _val0;
        
        public GroupMiniV2025R0? GroupMiniV2025R0 => _val1;
        
        public HubAccessGranteeV2025R0(HubCollaborationUserV2025R0 value) : base(value) {}
        
        public HubAccessGranteeV2025R0(GroupMiniV2025R0 value) : base(value) {}
        
        public static implicit operator HubAccessGranteeV2025R0(HubCollaborationUserV2025R0 value) => new HubAccessGranteeV2025R0(value);
        
        public static implicit operator HubAccessGranteeV2025R0(GroupMiniV2025R0 value) => new HubAccessGranteeV2025R0(value);
        
        class HubAccessGranteeV2025R0Converter : JsonConverter<HubAccessGranteeV2025R0> {
            public override HubAccessGranteeV2025R0 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "user":
                            return JsonSerializer.Deserialize<HubCollaborationUserV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to HubCollaborationUserV2025R0");
                        case "group":
                            return JsonSerializer.Deserialize<GroupMiniV2025R0>(document) ?? throw new Exception($"Could not deserialize {document} to GroupMiniV2025R0");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, HubAccessGranteeV2025R0? value, JsonSerializerOptions options) {
                if (value?.HubCollaborationUserV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.HubCollaborationUserV2025R0, options);
                    return;
                }
                if (value?.GroupMiniV2025R0 != null) {
                    JsonSerializer.Serialize(writer, value.GroupMiniV2025R0, options);
                    return;
                }
            }

        }

    }
}