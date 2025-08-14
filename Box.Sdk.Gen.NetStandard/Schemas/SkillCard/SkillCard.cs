using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(SkillCardConverter))]
    public class SkillCard {
        internal OneOf<KeywordSkillCard, TimelineSkillCard, TranscriptSkillCard, StatusSkillCard> _oneOf;
        
        public KeywordSkillCard KeywordSkillCard => _oneOf._val0;
        
        public TimelineSkillCard TimelineSkillCard => _oneOf._val1;
        
        public TranscriptSkillCard TranscriptSkillCard => _oneOf._val2;
        
        public StatusSkillCard StatusSkillCard => _oneOf._val3;
        
        public SkillCard(KeywordSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, TimelineSkillCard, TranscriptSkillCard, StatusSkillCard>(value);}
        
        public SkillCard(TimelineSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, TimelineSkillCard, TranscriptSkillCard, StatusSkillCard>(value);}
        
        public SkillCard(TranscriptSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, TimelineSkillCard, TranscriptSkillCard, StatusSkillCard>(value);}
        
        public SkillCard(StatusSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, TimelineSkillCard, TranscriptSkillCard, StatusSkillCard>(value);}
        
        public static implicit operator SkillCard(KeywordSkillCard value) => new SkillCard(value);
        
        public static implicit operator SkillCard(TimelineSkillCard value) => new SkillCard(value);
        
        public static implicit operator SkillCard(TranscriptSkillCard value) => new SkillCard(value);
        
        public static implicit operator SkillCard(StatusSkillCard value) => new SkillCard(value);
        
        class SkillCardConverter : JsonConverter<SkillCard> {
            public override SkillCard Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("skill_card_type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "keyword":
                                return JsonSerializer.Deserialize<KeywordSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to KeywordSkillCard");
                            case "timeline":
                                return JsonSerializer.Deserialize<TimelineSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to TimelineSkillCard");
                            case "transcript":
                                return JsonSerializer.Deserialize<TranscriptSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to TranscriptSkillCard");
                            case "status":
                                return JsonSerializer.Deserialize<StatusSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to StatusSkillCard");
                        }
                    }
                    throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
                }
            }

            public override void Write(Utf8JsonWriter writer, SkillCard value, JsonSerializerOptions options) {
                if (value?.KeywordSkillCard != null) {
                    JsonSerializer.Serialize(writer, value.KeywordSkillCard, options);
                    return;
                }
                if (value?.TimelineSkillCard != null) {
                    JsonSerializer.Serialize(writer, value.TimelineSkillCard, options);
                    return;
                }
                if (value?.TranscriptSkillCard != null) {
                    JsonSerializer.Serialize(writer, value.TranscriptSkillCard, options);
                    return;
                }
                if (value?.StatusSkillCard != null) {
                    JsonSerializer.Serialize(writer, value.StatusSkillCard, options);
                    return;
                }
            }

        }

    }
}