using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EventSource : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isparentSet")]
        protected bool _isParentSet { get; set; }

        protected FolderMini? _parent { get; set; }

        /// <summary>
        /// The type of the item that the event
        /// represents. Can be `file` or `folder`.
        /// </summary>
        [JsonPropertyName("item_type")]
        [JsonConverter(typeof(StringEnumConverter<EventSourceItemTypeField>))]
        public StringEnum<EventSourceItemTypeField> ItemType { get; }

        /// <summary>
        /// The unique identifier that represents the
        /// item.
        /// </summary>
        [JsonPropertyName("item_id")]
        public string ItemId { get; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [JsonPropertyName("item_name")]
        public string ItemName { get; }

        /// <summary>
        /// The object containing classification information for the item that
        /// triggered the event. This field will not appear if the item does not
        /// have a classification set.
        /// </summary>
        [JsonPropertyName("classification")]
        public EventSourceClassificationField? Classification { get; init; }

        [JsonPropertyName("parent")]
        public FolderMini? Parent { get => _parent; init { _parent = value; _isParentSet = true; } }

        [JsonPropertyName("owned_by")]
        public UserMini? OwnedBy { get; init; }

        public EventSource(EventSourceItemTypeField itemType, string itemId, string itemName) {
            ItemType = itemType;
            ItemId = itemId;
            ItemName = itemName;
        }
        
        [JsonConstructorAttribute]
        internal EventSource(StringEnum<EventSourceItemTypeField> itemType, string itemId, string itemName) {
            ItemType = itemType;
            ItemId = itemId;
            ItemName = itemName;
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