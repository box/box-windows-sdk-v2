using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationUpdateRequestV2025R0 : ISerializable {
        /// <summary>
        /// The level of access granted to a Box Hub.
        /// Possible values are `editor`, `viewer`, and `co-owner`.
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; init; }

        public HubCollaborationUpdateRequestV2025R0() {
            
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