using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiSingleAgentResponse : ISerializable {
        /// <summary>
        /// The unique identifier of the AI Agent.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of agent used to handle queries.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiSingleAgentResponseTypeField>))]
        public StringEnum<AiSingleAgentResponseTypeField>? Type { get; init; }

        /// <summary>
        /// The provider of the AI Agent.
        /// </summary>
        [JsonPropertyName("origin")]
        public string Origin { get; }

        /// <summary>
        /// The name of the AI Agent.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// The state of the AI Agent. Possible values are: `enabled`, `disabled`, and `enabled_for_selected_users`.
        /// </summary>
        [JsonPropertyName("access_state")]
        public string AccessState { get; }

        /// <summary>
        /// The user who created this agent.
        /// </summary>
        [JsonPropertyName("created_by")]
        public UserBase? CreatedBy { get; init; }

        /// <summary>
        /// The ISO date-time formatted timestamp of when this AI agent was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The user who most recently modified this agent.
        /// </summary>
        [JsonPropertyName("modified_by")]
        public UserBase? ModifiedBy { get; init; }

        /// <summary>
        /// The ISO date-time formatted timestamp of when this AI agent was recently modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        /// <summary>
        /// The icon reference of the AI Agent.
        /// </summary>
        [JsonPropertyName("icon_reference")]
        public string? IconReference { get; init; }

        /// <summary>
        /// List of allowed users or groups.
        /// </summary>
        [JsonPropertyName("allowed_entities")]
        public IReadOnlyList<AiAgentAllowedEntity>? AllowedEntities { get; init; }

        public AiSingleAgentResponse(string id, string origin, string name, string accessState) {
            Id = id;
            Origin = origin;
            Name = name;
            AccessState = accessState;
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