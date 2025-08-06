using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubV2025R0 : HubBaseV2025R0, ISerializable {
        /// <summary>
        /// The title given to the hub.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// The description of the hub. First 200 characters are returned.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The date and time when the folder was created. This value may
        /// be `null` for some folders such as the root folder or the trash
        /// folder.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the hub was last updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserMiniV2025R0 CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public UserMiniV2025R0 UpdatedBy { get; set; }

        /// <summary>
        /// The number of views for the hub.
        /// </summary>
        [JsonPropertyName("view_count")]
        public int? ViewCount { get; set; }

        /// <summary>
        /// Indicates if AI features are enabled for the hub.
        /// </summary>
        [JsonPropertyName("is_ai_enabled")]
        public bool? IsAiEnabled { get; set; }

        /// <summary>
        /// Indicates if collaboration is restricted to the enterprise.
        /// </summary>
        [JsonPropertyName("is_collaboration_restricted_to_enterprise")]
        public bool? IsCollaborationRestrictedToEnterprise { get; set; }

        /// <summary>
        /// Indicates if non-owners can invite others to the hub.
        /// </summary>
        [JsonPropertyName("can_non_owners_invite")]
        public bool? CanNonOwnersInvite { get; set; }

        /// <summary>
        /// Indicates if a shared link can be created for the hub.
        /// </summary>
        [JsonPropertyName("can_shared_link_be_created")]
        public bool? CanSharedLinkBeCreated { get; set; }

        public HubV2025R0(string id, HubBaseV2025R0TypeField type = HubBaseV2025R0TypeField.Hubs) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal HubV2025R0(string id, StringEnum<HubBaseV2025R0TypeField> type) : base(id, type ?? new StringEnum<HubBaseV2025R0TypeField>(HubBaseV2025R0TypeField.Hubs)) {
            
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}