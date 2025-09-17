using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Outcome : ISerializable {
        /// <summary>
        /// ID of a specific outcome.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        [JsonPropertyName("collaborators")]
        public CollaboratorVariable? Collaborators { get; init; }

        [JsonPropertyName("completion_rule")]
        public CompletionRuleVariable? CompletionRule { get; init; }

        [JsonPropertyName("file_collaborator_role")]
        public RoleVariable? FileCollaboratorRole { get; init; }

        [JsonPropertyName("task_collaborators")]
        public CollaboratorVariable? TaskCollaborators { get; init; }

        [JsonPropertyName("role")]
        public RoleVariable? Role { get; init; }

        public Outcome(string id) {
            Id = id;
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