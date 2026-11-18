using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiSessionV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isclient_idSet")]
        protected bool _isClientIdSet { get; set; }

        protected string _name { get; set; }

        protected string _clientId { get; set; }

        /// <summary>
        /// The value is always `ai_session`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiSessionV2026R0TypeField>))]
        public StringEnum<AiSessionV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The unique identifier for the AI session. This is also the A2A
        /// `contextId` for the conversation. Treat the value as opaque.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The display name of the AI session. Sessions created through the A2A API
        /// are named automatically. The value is `null` only when no name was
        /// recorded.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get => _name; set { _name = value; _isNameSet = true; } }

        /// <summary>
        /// The origin of the AI session. Currently always `api`. New values
        /// may be added, so clients must tolerate values they do not recognize.
        /// </summary>
        [JsonPropertyName("source")]
        [JsonConverter(typeof(StringEnumConverter<AiSessionV2026R0SourceField>))]
        public StringEnum<AiSessionV2026R0SourceField> Source { get; set; }

        /// <summary>
        /// The OAuth client ID of the application that created the AI session, as
        /// shown in the Developer Console. The value is `null` if the application is
        /// not known.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string ClientId { get => _clientId; set { _clientId = value; _isClientIdSet = true; } }

        [JsonPropertyName("created_by")]
        public UserBaseV2026R0 CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public UserBaseV2026R0 UpdatedBy { get; set; }

        /// <summary>
        /// The date and time when the AI session was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the AI session was last updated. It advances on
        /// each turn of session activity, so it also serves as the most recently
        /// used ordering.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset UpdatedAt { get; set; }

        public AiSessionV2026R0(string id, UserBaseV2026R0 createdBy, UserBaseV2026R0 updatedBy, System.DateTimeOffset createdAt, System.DateTimeOffset updatedAt, AiSessionV2026R0TypeField type = AiSessionV2026R0TypeField.AiSession, AiSessionV2026R0SourceField source = AiSessionV2026R0SourceField.Api) {
            Type = type;
            Id = id;
            Source = source;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        [JsonConstructorAttribute]
        internal AiSessionV2026R0(string id, UserBaseV2026R0 createdBy, UserBaseV2026R0 updatedBy, System.DateTimeOffset createdAt, System.DateTimeOffset updatedAt, StringEnum<AiSessionV2026R0TypeField> type, StringEnum<AiSessionV2026R0SourceField> source) {
            Type = AiSessionV2026R0TypeField.AiSession;
            Id = id;
            Source = AiSessionV2026R0SourceField.Api;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
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