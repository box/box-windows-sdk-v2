using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AppItemEventSource : ISerializable {
        /// <summary>
        /// The id of the `AppItem`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of the source that this event represents. Can only be `app_item`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AppItemEventSourceTypeField>))]
        public StringEnum<AppItemEventSourceTypeField> Type { get; }

        /// <summary>
        /// The type of the `AppItem`.
        /// </summary>
        [JsonPropertyName("app_item_type")]
        public string AppItemType { get; }

        [JsonPropertyName("user")]
        public UserMini? User { get; init; }

        [JsonPropertyName("group")]
        public GroupMini? Group { get; init; }

        public AppItemEventSource(string id, string appItemType, AppItemEventSourceTypeField type = AppItemEventSourceTypeField.AppItem) {
            Id = id;
            Type = type;
            AppItemType = appItemType;
        }
        
        [JsonConstructorAttribute]
        internal AppItemEventSource(string id, string appItemType, StringEnum<AppItemEventSourceTypeField> type) {
            Id = id;
            Type = AppItemEventSourceTypeField.AppItem;
            AppItemType = appItemType;
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