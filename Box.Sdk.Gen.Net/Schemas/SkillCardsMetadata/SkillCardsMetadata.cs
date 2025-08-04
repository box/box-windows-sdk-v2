using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SkillCardsMetadata : ISerializable {
        /// <summary>
        /// Whether the user can edit this metadata.
        /// </summary>
        [JsonPropertyName("$canEdit")]
        public bool? CanEdit { get; init; }

        /// <summary>
        /// A UUID to identify the metadata object.
        /// </summary>
        [JsonPropertyName("$id")]
        public string? Id { get; init; }

        /// <summary>
        /// An ID for the parent folder.
        /// </summary>
        [JsonPropertyName("$parent")]
        public string? Parent { get; init; }

        /// <summary>
        /// An ID for the scope in which this template
        /// has been applied.
        /// </summary>
        [JsonPropertyName("$scope")]
        public string? Scope { get; init; }

        /// <summary>
        /// The name of the template.
        /// </summary>
        [JsonPropertyName("$template")]
        public string? Template { get; init; }

        /// <summary>
        /// A unique identifier for the "type" of this instance. This is an internal
        /// system property and should not be used by a client application.
        /// </summary>
        [JsonPropertyName("$type")]
        public string? Type { get; init; }

        /// <summary>
        /// The last-known version of the template of the object. This is an internal
        /// system property and should not be used by a client application.
        /// </summary>
        [JsonPropertyName("$typeVersion")]
        public long? TypeVersion { get; init; }

        /// <summary>
        /// The version of the metadata object. Starts at 0 and increases every time
        /// a user-defined property is modified.
        /// </summary>
        [JsonPropertyName("$version")]
        public long? Version { get; init; }

        /// <summary>
        /// A list of Box Skill cards that have been applied to this file.
        /// </summary>
        [JsonPropertyName("cards")]
        public IReadOnlyList<KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard>? Cards { get; init; }

        public SkillCardsMetadata() {
            
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