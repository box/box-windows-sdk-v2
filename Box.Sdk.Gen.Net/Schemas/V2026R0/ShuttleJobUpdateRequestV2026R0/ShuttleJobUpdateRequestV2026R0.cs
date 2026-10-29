using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobUpdateRequestV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscommentSet")]
        protected bool _isCommentSet { get; set; }

        protected string? _comment { get; set; }

        /// <summary>
        /// The human-readable name of the shuttle job.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// An optional free-form comment describing the shuttle job.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get => _comment; init { _comment = value; _isCommentSet = true; } }

        /// <summary>
        /// A list of free-form tags associated with the shuttle job.
        /// </summary>
        [JsonPropertyName("tags")]
        public IReadOnlyList<string>? Tags { get; init; }

        /// <summary>
        /// The account mapping configuration that controls how source identities
        /// and permissions are translated into Box.
        /// </summary>
        [JsonPropertyName("account_mapping")]
        public ShuttleJobUpdateRequestV2026R0AccountMappingField? AccountMapping { get; init; }

        /// <summary>
        /// The tunable settings that control how the shuttle job runs.
        /// </summary>
        [JsonPropertyName("settings")]
        public ShuttleJobUpdateRequestV2026R0SettingsField? Settings { get; init; }

        public ShuttleJobUpdateRequestV2026R0() {
            
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