using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Invite : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isauto_roll_in_atSet")]
        protected bool _isAutoRollInAtSet { get; set; }

        protected System.DateTimeOffset? _autoRollInAt { get; set; }

        /// <summary>
        /// The unique identifier for this invite.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `invite`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<InviteTypeField>))]
        public StringEnum<InviteTypeField> Type { get; set; }

        /// <summary>
        /// A representation of a Box enterprise.
        /// </summary>
        [JsonPropertyName("invited_to")]
        public InviteInvitedToField InvitedTo { get; set; }

        [JsonPropertyName("actionable_by")]
        public UserMini ActionableBy { get; set; }

        [JsonPropertyName("invited_by")]
        public UserMini InvitedBy { get; set; }

        /// <summary>
        /// The status of the invite.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// When the invite was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// When the invite was modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; set; }

        /// <summary>
        /// The date and time when the user is added to the enterprise
        /// automatically, or `null` if the user has to accept the invite.
        /// </summary>
        [JsonPropertyName("auto_roll_in_at")]
        public System.DateTimeOffset? AutoRollInAt { get => _autoRollInAt; set { _autoRollInAt = value; _isAutoRollInAtSet = true; } }

        /// <summary>
        /// Whether the user is added to the enterprise automatically, without
        /// having to accept the invite. When `true`, `auto_roll_in_at` contains
        /// the date and time of the roll-in.
        /// </summary>
        [JsonPropertyName("is_auto_roll_in")]
        public bool? IsAutoRollIn { get; set; }

        public Invite(string id, InviteTypeField type = InviteTypeField.Invite) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal Invite(string id, StringEnum<InviteTypeField> type) {
            Id = id;
            Type = InviteTypeField.Invite;
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