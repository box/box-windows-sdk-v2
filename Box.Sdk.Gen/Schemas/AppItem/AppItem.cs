using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AppItem : ISerializable {
        /// <summary>
        /// The unique identifier for this app item.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `app_item`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AppItemTypeField>))]
        public StringEnum<AppItemTypeField> Type { get; }

        /// <summary>
        /// The type of the app that owns this app item.
        /// </summary>
        [JsonPropertyName("application_type")]
        public string ApplicationType { get; }

        public AppItem(string id, string applicationType, AppItemTypeField type = AppItemTypeField.AppItem) {
            Id = id;
            Type = type;
            ApplicationType = applicationType;
        }
        
        [JsonConstructorAttribute]
        internal AppItem(string id, string applicationType, StringEnum<AppItemTypeField> type) {
            Id = id;
            Type = AppItemTypeField.AppItem;
            ApplicationType = applicationType;
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