using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AiTaxonomySourceConverter))]
    public class AiTaxonomySource {
        internal OneOf<AiTaxonomyReference, AiTaxonomyFileReference> _oneOf;
        
        public AiTaxonomyReference AiTaxonomyReference => _oneOf._val0;
        
        public AiTaxonomyFileReference AiTaxonomyFileReference => _oneOf._val1;
        
        public AiTaxonomySource(AiTaxonomyReference value) {_oneOf = new OneOf<AiTaxonomyReference, AiTaxonomyFileReference>(value);}
        
        public AiTaxonomySource(AiTaxonomyFileReference value) {_oneOf = new OneOf<AiTaxonomyReference, AiTaxonomyFileReference>(value);}
        
        public static implicit operator AiTaxonomySource(AiTaxonomyReference value) => new AiTaxonomySource(value);
        
        public static implicit operator AiTaxonomySource(AiTaxonomyFileReference value) => new AiTaxonomySource(value);
        
        class AiTaxonomySourceConverter : JsonConverter<AiTaxonomySource> {
            public override AiTaxonomySource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "taxonomy":
                                return JsonSerializer.Deserialize<AiTaxonomyReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiTaxonomyReference");
                            case "file":
                                return JsonSerializer.Deserialize<AiTaxonomyFileReference>(document) ?? throw new Exception($"Could not deserialize {document} to AiTaxonomyFileReference");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, AiTaxonomySource value, JsonSerializerOptions options) {
                if (value?.AiTaxonomyReference != null) {
                    JsonSerializer.Serialize(writer, value.AiTaxonomyReference, options);
                    return;
                }
                if (value?.AiTaxonomyFileReference != null) {
                    JsonSerializer.Serialize(writer, value.AiTaxonomyFileReference, options);
                    return;
                }
            }

        }

    }
}