using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class TranscriptSkillCardEntriesField : ISerializable {
        /// <summary>
        /// The text of the entry. This would be the transcribed text assigned
        /// to the entry on the timeline.
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; init; }

        /// <summary>
        /// Defines when a transcribed bit of text appears. This only includes a
        /// start time and no end time.
        /// </summary>
        [JsonPropertyName("appears")]
        public IReadOnlyList<TranscriptSkillCardEntriesAppearsField>? Appears { get; init; }

        public TranscriptSkillCardEntriesField() {
            
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}