using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldListV2025R0 : ISerializable {
        /// <summary>
        /// Unique identifier for the shield list.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// Type of the object.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; }

        /// <summary>
        /// Name of the shield list.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("enterprise")]
        public EnterpriseReferenceV2025R0 Enterprise { get; }

        /// <summary>
        /// Description of Shield List.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// ISO date time string when this shield list object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// ISO date time string when this shield list object was updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset UpdatedAt { get; }

        [JsonPropertyName("content")]
        public ShieldListContentV2025R0 Content { get; }

        public ShieldListV2025R0(string id, string type, string name, EnterpriseReferenceV2025R0 enterprise, System.DateTimeOffset createdAt, System.DateTimeOffset updatedAt, ShieldListContentV2025R0 content) {
            Id = id;
            Type = type;
            Name = name;
            Enterprise = enterprise;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Content = content;
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