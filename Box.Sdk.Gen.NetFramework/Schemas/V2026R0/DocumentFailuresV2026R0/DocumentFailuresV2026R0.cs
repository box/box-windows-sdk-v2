using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentFailuresV2026R0 : ISerializable {
        /// <summary>
        /// A list of errors that occurred during the document job.
        /// </summary>
        [JsonPropertyName("errors")]
        public IReadOnlyList<string> Errors { get; set; }

        /// <summary>
        /// A list of warnings that occurred during the document job.
        /// </summary>
        [JsonPropertyName("warnings")]
        public IReadOnlyList<string> Warnings { get; set; }

        public DocumentFailuresV2026R0(IReadOnlyList<string> errors, IReadOnlyList<string> warnings) {
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