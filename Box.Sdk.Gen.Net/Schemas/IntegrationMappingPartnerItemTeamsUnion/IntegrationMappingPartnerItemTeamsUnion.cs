using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(IntegrationMappingPartnerItemTeamsUnionConverter))]
    public class IntegrationMappingPartnerItemTeamsUnion : OneOf<IntegrationMappingPartnerItemTeams> {
        public IntegrationMappingPartnerItemTeams? IntegrationMappingPartnerItemTeams => _val0;
        
        public IntegrationMappingPartnerItemTeamsUnion(IntegrationMappingPartnerItemTeams value) : base(value) {}
        
        public static implicit operator IntegrationMappingPartnerItemTeamsUnion(IntegrationMappingPartnerItemTeams value) => new IntegrationMappingPartnerItemTeamsUnion(value);
        
        class IntegrationMappingPartnerItemTeamsUnionConverter : JsonConverter<IntegrationMappingPartnerItemTeamsUnion> {
            public override IntegrationMappingPartnerItemTeamsUnion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "channel":
                            return JsonSerializer.Deserialize<IntegrationMappingPartnerItemTeams>(document) ?? throw new Exception($"Could not deserialize {document} to IntegrationMappingPartnerItemTeams");
                        case "team":
                            return JsonSerializer.Deserialize<IntegrationMappingPartnerItemTeams>(document) ?? throw new Exception($"Could not deserialize {document} to IntegrationMappingPartnerItemTeams");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, IntegrationMappingPartnerItemTeamsUnion? value, JsonSerializerOptions options) {
                if (value?.IntegrationMappingPartnerItemTeams != null) {
                    JsonSerializer.Serialize(writer, value.IntegrationMappingPartnerItemTeams, options);
                    return;
                }
            }

        }

    }
}