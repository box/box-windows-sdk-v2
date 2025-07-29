using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class RecentItem : ISerializable {
        /// <summary>
        /// The value will always be `recent_item`.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("item")]
        public FileFullOrFolderFullOrWebLink? Item { get; init; }

        /// <summary>
        /// The most recent type of access the user performed on
        /// the item.
        /// </summary>
        [JsonPropertyName("interaction_type")]
        [JsonConverter(typeof(StringEnumConverter<RecentItemInteractionTypeField>))]
        public StringEnum<RecentItemInteractionTypeField>? InteractionType { get; init; }

        /// <summary>
        /// The time of the most recent interaction.
        /// </summary>
        [JsonPropertyName("interacted_at")]
        public System.DateTimeOffset? InteractedAt { get; init; }

        /// <summary>
        /// If the item was accessed through a shared link it will appear here,
        /// otherwise this will be null.
        /// </summary>
        [JsonPropertyName("interaction_shared_link")]
        public string? InteractionSharedLink { get; init; }

        public RecentItem() {
            
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