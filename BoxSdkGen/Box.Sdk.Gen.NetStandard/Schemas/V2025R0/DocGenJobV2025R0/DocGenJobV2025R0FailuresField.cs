using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenJobV2025R0FailuresField : ISerializable {
        /// <summary>
        /// A list of errors that occurred during document generation.
        /// </summary>
        [JsonPropertyName("errors")]
        public IReadOnlyList<string> Errors { get; set; }

        /// <summary>
        /// A list of warnings that occurred during document generation.
        /// </summary>
        [JsonPropertyName("warnings")]
        public IReadOnlyList<string> Warnings { get; set; }

        public DocGenJobV2025R0FailuresField(IReadOnlyList<string> errors, IReadOnlyList<string> warnings) {
            Errors = errors;
            Warnings = warnings;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}