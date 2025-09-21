using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class CollaborationAllowlistEntry : ISerializable {
        /// <summary>
        /// The unique identifier for this entry.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `collaboration_whitelist_entry`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CollaborationAllowlistEntryTypeField>))]
        public StringEnum<CollaborationAllowlistEntryTypeField>? Type { get; init; }

        /// <summary>
        /// The whitelisted domain.
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain { get; init; }

        /// <summary>
        /// The direction of the collaborations to allow.
        /// </summary>
        [JsonPropertyName("direction")]
        [JsonConverter(typeof(StringEnumConverter<CollaborationAllowlistEntryDirectionField>))]
        public StringEnum<CollaborationAllowlistEntryDirectionField>? Direction { get; init; }

        [JsonPropertyName("enterprise")]
        public CollaborationAllowlistEntryEnterpriseField? Enterprise { get; init; }

        /// <summary>
        /// The time the entry was created at.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        public CollaborationAllowlistEntry() {
            
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