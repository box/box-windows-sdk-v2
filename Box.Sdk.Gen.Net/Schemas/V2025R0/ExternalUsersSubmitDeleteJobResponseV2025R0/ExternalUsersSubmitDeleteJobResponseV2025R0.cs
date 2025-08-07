using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ExternalUsersSubmitDeleteJobResponseV2025R0 : ISerializable {
        /// <summary>
        /// Array of results of each external user deletion request.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<ExternalUserDeletionResultV2025R0> Entries { get; }

        public ExternalUsersSubmitDeleteJobResponseV2025R0(IReadOnlyList<ExternalUserDeletionResultV2025R0> entries) {
            Entries = entries;
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