using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubV2025R0 : HubBaseV2025R0, ISerializable {
        /// <summary>
        /// The title given to the Box Hub.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        /// The description of the Box Hub. First 200 characters are returned.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The date and time when the folder was created. This value may
        /// be `null` for some folders such as the root folder or the trash
        /// folder.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The date and time when the Box Hub was last updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset? UpdatedAt { get; init; }

        [JsonPropertyName("created_by")]
        public UserMiniV2025R0? CreatedBy { get; init; }

        [JsonPropertyName("updated_by")]
        public UserMiniV2025R0? UpdatedBy { get; init; }

        /// <summary>
        /// The number of views for the Box Hub.
        /// </summary>
        [JsonPropertyName("view_count")]
        public int? ViewCount { get; init; }

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

        public HubV2025R0(string id, HubBaseV2025R0TypeField type = HubBaseV2025R0TypeField.Hubs) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal HubV2025R0(string id, StringEnum<HubBaseV2025R0TypeField> type) : base(id, type ?? new StringEnum<HubBaseV2025R0TypeField>(HubBaseV2025R0TypeField.Hubs)) {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}