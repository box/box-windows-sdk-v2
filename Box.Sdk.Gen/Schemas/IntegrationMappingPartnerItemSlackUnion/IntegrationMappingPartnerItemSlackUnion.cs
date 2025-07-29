using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(IntegrationMappingPartnerItemSlackUnionConverter))]
    public class IntegrationMappingPartnerItemSlackUnion : OneOf<IntegrationMappingPartnerItemSlack> {
        public IntegrationMappingPartnerItemSlack? IntegrationMappingPartnerItemSlack => _val0;
        
        public IntegrationMappingPartnerItemSlackUnion(IntegrationMappingPartnerItemSlack value) : base(value) {}
        
        public static implicit operator IntegrationMappingPartnerItemSlackUnion(IntegrationMappingPartnerItemSlack value) => new IntegrationMappingPartnerItemSlackUnion(value);
        
        class IntegrationMappingPartnerItemSlackUnionConverter : JsonConverter<IntegrationMappingPartnerItemSlackUnion> {
            public override IntegrationMappingPartnerItemSlackUnion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "channel":
                            return JsonSerializer.Deserialize<IntegrationMappingPartnerItemSlack>(document) ?? throw new Exception($"Could not deserialize {document} to IntegrationMappingPartnerItemSlack");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, IntegrationMappingPartnerItemSlackUnion? value, JsonSerializerOptions options) {
                if (value?.IntegrationMappingPartnerItemSlack != null) {
                    JsonSerializer.Serialize(writer, value.IntegrationMappingPartnerItemSlack, options);
                    return;
                }
            }

        }

    }
}