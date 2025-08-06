using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class TerminateGroupsSessionsRequestBody : ISerializable {
        /// <summary>
        /// A list of group IDs.
        /// </summary>
        [JsonPropertyName("group_ids")]
        public IReadOnlyList<string> GroupIds { get; }

        public TerminateGroupsSessionsRequestBody(IReadOnlyList<string> groupIds) {
            GroupIds = groupIds;
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