using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationCreateRequestV2025R0 : ISerializable {
        /// <summary>
        /// Box Hubs reference.
        /// </summary>
        [JsonPropertyName("hub")]
        public HubCollaborationCreateRequestV2025R0HubField Hub { get; }

        /// <summary>
        /// The user or group who gets access to the item.
        /// </summary>
        [JsonPropertyName("accessible_by")]
        public HubCollaborationCreateRequestV2025R0AccessibleByField AccessibleBy { get; }

        /// <summary>
        /// The level of access granted to a Box Hub.
        /// Possible values are `editor`, `viewer`, and `co-owner`.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; }

        public HubCollaborationCreateRequestV2025R0(HubCollaborationCreateRequestV2025R0HubField hub, HubCollaborationCreateRequestV2025R0AccessibleByField accessibleBy, string role) {
            Hub = hub;
            AccessibleBy = accessibleBy;
            Role = role;
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