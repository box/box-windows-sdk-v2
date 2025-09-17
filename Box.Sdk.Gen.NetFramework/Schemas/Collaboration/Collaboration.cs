using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Collaboration : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isitemSet")]
        protected bool _isItemSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isapp_itemSet")]
        protected bool _isAppItemSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isinvite_emailSet")]
        protected bool _isInviteEmailSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isexpires_atSet")]
        protected bool _isExpiresAtSet { get; set; }

        protected CollaborationItem _item { get; set; }

        protected AppItem _appItem { get; set; }

        protected string _inviteEmail { get; set; }

        protected System.DateTimeOffset? _expiresAt { get; set; }

        /// <summary>
        /// The unique identifier for this collaboration.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `collaboration`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CollaborationTypeField>))]
        public StringEnum<CollaborationTypeField> Type { get; set; }

        [JsonPropertyName("item")]
        public CollaborationItem Item { get => _item; set { _item = value; _isItemSet = true; } }

        [JsonPropertyName("app_item")]
        public AppItem AppItem { get => _appItem; set { _appItem = value; _isAppItemSet = true; } }

        [JsonPropertyName("accessible_by")]
        public CollaborationAccessGrantee AccessibleBy { get; set; }

        /// <summary>
        /// The email address used to invite an unregistered collaborator, if
        /// they are not a registered user.
        /// </summary>
        [JsonPropertyName("invite_email")]
        public string InviteEmail { get => _inviteEmail; set { _inviteEmail = value; _isInviteEmailSet = true; } }

        /// <summary>
        /// The level of access granted.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonConverter(typeof(StringEnumConverter<CollaborationRoleField>))]
        public StringEnum<CollaborationRoleField> Role { get; set; }

        /// <summary>
        /// When the collaboration will expire, or `null` if no expiration
        /// date is set.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset? ExpiresAt { get => _expiresAt; set { _expiresAt = value; _isExpiresAtSet = true; } }

        /// <summary>
        /// If set to `true`, collaborators have access to
        /// shared items, but such items won't be visible in the
        /// All Files list. Additionally, collaborators won't
        /// see the path to the root folder for the
        /// shared item.
        /// </summary>
        [JsonPropertyName("is_access_only")]
        public bool? IsAccessOnly { get; set; }

        /// <summary>
        /// The status of the collaboration invitation. If the status
        /// is `pending`, `login` and `name` return an empty string.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<CollaborationStatusField>))]
        public StringEnum<CollaborationStatusField> Status { get; set; }

        /// <summary>
        /// When the `status` of the collaboration object changed to
        /// `accepted` or `rejected`.
        /// </summary>
        [JsonPropertyName("acknowledged_at")]
        public System.DateTimeOffset? AcknowledgedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserCollaborations CreatedBy { get; set; }

        /// <summary>
        /// When the collaboration object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// When the collaboration object was last modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; set; }

        [JsonPropertyName("acceptance_requirements_status")]
        public CollaborationAcceptanceRequirementsStatusField AcceptanceRequirementsStatus { get; set; }

        public Collaboration(string id, CollaborationTypeField type = CollaborationTypeField.Collaboration) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal Collaboration(string id, StringEnum<CollaborationTypeField> type) {
            Id = id;
            Type = CollaborationTypeField.Collaboration;
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