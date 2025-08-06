using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier for this collaboration.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `hub_collaboration`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubCollaborationV2025R0TypeField>))]
        public StringEnum<HubCollaborationV2025R0TypeField> Type { get; }

        [JsonPropertyName("hub")]
        public HubBaseV2025R0? Hub { get; init; }

        [JsonPropertyName("accessible_by")]
        public HubAccessGranteeV2025R0? AccessibleBy { get; init; }

        /// <summary>
        /// The level of access granted to hub.
        /// Possible values are `editor`, `viewer`, and `co-owner`.
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; init; }

        /// <summary>
        /// The status of the collaboration invitation. If the status
        /// is `pending`, `login` and `name` return an empty string.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<HubCollaborationV2025R0StatusField>))]
        public StringEnum<HubCollaborationV2025R0StatusField>? Status { get; init; }

        [JsonPropertyName("acceptance_requirements_status")]
        public HubCollaborationV2025R0AcceptanceRequirementsStatusField? AcceptanceRequirementsStatus { get; init; }

        public HubCollaborationV2025R0(string id, HubCollaborationV2025R0TypeField type = HubCollaborationV2025R0TypeField.HubCollaboration) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal HubCollaborationV2025R0(string id, StringEnum<HubCollaborationV2025R0TypeField> type) {
            Id = id;
            Type = HubCollaborationV2025R0TypeField.HubCollaboration;
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