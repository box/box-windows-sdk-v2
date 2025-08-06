using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TermsOfServiceUserStatuses : ISerializable {
        /// <summary>
        /// The total number of objects.
        /// </summary>
        [JsonPropertyName("total_count")]
        public long? TotalCount { get; set; }

        /// <summary>
        /// A list of terms of service user statuses.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<TermsOfServiceUserStatus> Entries { get; set; }

        public TermsOfServiceUserStatuses() {
            
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