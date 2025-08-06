using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCardConverter))]
    public class KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard {
        internal OneOf<KeywordSkillCard, StatusSkillCard, TimelineSkillCard, TranscriptSkillCard> _oneOf;
        
        public KeywordSkillCard? KeywordSkillCard => _oneOf._val0;
        
        public StatusSkillCard? StatusSkillCard => _oneOf._val1;
        
        public TimelineSkillCard? TimelineSkillCard => _oneOf._val2;
        
        public TranscriptSkillCard? TranscriptSkillCard => _oneOf._val3;
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(KeywordSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, StatusSkillCard, TimelineSkillCard, TranscriptSkillCard>(value);}
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(StatusSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, StatusSkillCard, TimelineSkillCard, TranscriptSkillCard>(value);}
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(TimelineSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, StatusSkillCard, TimelineSkillCard, TranscriptSkillCard>(value);}
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(TranscriptSkillCard value) {_oneOf = new OneOf<KeywordSkillCard, StatusSkillCard, TimelineSkillCard, TranscriptSkillCard>(value);}
        
        public static implicit operator KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(KeywordSkillCard value) => new KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(value);
        
        public static implicit operator KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(StatusSkillCard value) => new KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(value);
        
        public static implicit operator KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(TimelineSkillCard value) => new KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(value);
        
        public static implicit operator KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(TranscriptSkillCard value) => new KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(value);
        
        class KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCardConverter : JsonConverter<KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard> {
            public override KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("skill_card_type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "keyword":
                            return JsonSerializer.Deserialize<KeywordSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to KeywordSkillCard");
                        case "status":
                            return JsonSerializer.Deserialize<StatusSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to StatusSkillCard");
                        case "timeline":
                            return JsonSerializer.Deserialize<TimelineSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to TimelineSkillCard");
                        case "transcript":
                            return JsonSerializer.Deserialize<TranscriptSkillCard>(document) ?? throw new Exception($"Could not deserialize {document} to TranscriptSkillCard");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard? value, JsonSerializerOptions options) {
                if (value?.KeywordSkillCard != null) {
                    JsonSerializer.Serialize(writer, value.KeywordSkillCard, options);
                    return;
                }
                if (value?.StatusSkillCard != null) {
                    JsonSerializer.Serialize(writer, value.StatusSkillCard, options);
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
            }

        }

    }
}