using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCardConverter))]
    public class KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard : OneOf<KeywordSkillCard, StatusSkillCard, TimelineSkillCard, TranscriptSkillCard> {
        public KeywordSkillCard? KeywordSkillCard => _val0;
        
        public StatusSkillCard? StatusSkillCard => _val1;
        
        public TimelineSkillCard? TimelineSkillCard => _val2;
        
        public TranscriptSkillCard? TranscriptSkillCard => _val3;
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(KeywordSkillCard value) : base(value) {}
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(StatusSkillCard value) : base(value) {}
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(TimelineSkillCard value) : base(value) {}
        
        public KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard(TranscriptSkillCard value) : base(value) {}
        
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