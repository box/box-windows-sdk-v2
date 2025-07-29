using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class HubUpdateRequestV2025R0 : ISerializable {
        /// <summary>
        /// Title of the Hub. It cannot be empty and should be less than 50 characters.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        /// Description of the Hub.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// Indicates if AI features are enabled for the Hub.
        /// </summary>
        [JsonPropertyName("is_ai_enabled")]
        public bool? IsAiEnabled { get; init; }

        /// <summary>
        /// Indicates if collaboration is restricted to the enterprise.
        /// </summary>
        [JsonPropertyName("is_collaboration_restricted_to_enterprise")]
        public bool? IsCollaborationRestrictedToEnterprise { get; init; }

        /// <summary>
        /// Indicates if non-owners can invite others to the Hub.
        /// </summary>
        [JsonPropertyName("can_non_owners_invite")]
        public bool? CanNonOwnersInvite { get; init; }

        /// <summary>
        /// Indicates if a shared link can be created for the Hub.
        /// </summary>
        [JsonPropertyName("can_shared_link_be_created")]
        public bool? CanSharedLinkBeCreated { get; init; }

        public HubUpdateRequestV2025R0() {
            
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