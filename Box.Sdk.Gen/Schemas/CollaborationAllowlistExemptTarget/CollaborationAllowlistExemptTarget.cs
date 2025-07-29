using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class CollaborationAllowlistExemptTarget : ISerializable {
        /// <summary>
        /// The unique identifier for this exemption.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `collaboration_whitelist_exempt_target`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CollaborationAllowlistExemptTargetTypeField>))]
        public StringEnum<CollaborationAllowlistExemptTargetTypeField>? Type { get; init; }

        [JsonPropertyName("enterprise")]
        public CollaborationAllowlistExemptTargetEnterpriseField? Enterprise { get; init; }

        [JsonPropertyName("user")]
        public UserMini? User { get; init; }

        /// <summary>
        /// The time the entry was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The time the entry was modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        public CollaborationAllowlistExemptTarget() {
            
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