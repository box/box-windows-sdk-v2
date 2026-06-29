using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class HubUpdateRequestV2025R0 : ISerializable {
        /// <summary>
        /// Title of the Box Hub. It cannot be empty and should be less than 50 characters.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        /// Description of the Box Hub.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// Indicates if AI features are enabled for the Box Hub.
        /// </summary>
        [JsonPropertyName("is_ai_enabled")]
        public bool? IsAiEnabled { get; init; }

        /// <summary>
        /// Indicates if collaboration is restricted to the enterprise.
        /// </summary>
        [JsonPropertyName("is_collaboration_restricted_to_enterprise")]
        public bool? IsCollaborationRestrictedToEnterprise { get; init; }

        /// <summary>
        /// Indicates if non-owners can invite others to the Box Hub.
        /// </summary>
        [JsonPropertyName("can_non_owners_invite")]
        public bool? CanNonOwnersInvite { get; init; }

        /// <summary>
        /// Indicates if a shared link can be created for the Box Hub.
        /// </summary>
        [JsonPropertyName("can_shared_link_be_created")]
        public bool? CanSharedLinkBeCreated { get; init; }

        /// <summary>
        /// Indicates if a public shared link can be created for the Box Hub.
        /// </summary>
        [JsonPropertyName("can_public_shared_link_be_created")]
        public bool? CanPublicSharedLinkBeCreated { get; init; }

        /// <summary>
        /// Specifies who is allowed to copy the Box Hub.
        /// 
        /// * `all` - Any user with access to the Hub can copy it.
        /// * `company` - Only users within the same enterprise as the Hub can copy it.
        /// * `none` - No one can copy the Hub.
        /// </summary>
        [JsonPropertyName("copy_hub_access")]
        [JsonConverter(typeof(StringEnumConverter<HubUpdateRequestV2025R0CopyHubAccessField>))]
        public StringEnum<HubUpdateRequestV2025R0CopyHubAccessField>? CopyHubAccess { get; init; }

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