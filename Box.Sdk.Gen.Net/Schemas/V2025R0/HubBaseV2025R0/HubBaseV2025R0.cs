using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class HubBaseV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier that represent a hub.
        /// 
        /// The ID for any hub can be determined
        /// by visiting a hub in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/hubs/123`
        /// the `hub_id` is `123`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `hubs`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubBaseV2025R0TypeField>))]
        public StringEnum<HubBaseV2025R0TypeField> Type { get; }

        public HubBaseV2025R0(string id, HubBaseV2025R0TypeField type = HubBaseV2025R0TypeField.Hubs) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal HubBaseV2025R0(string id, StringEnum<HubBaseV2025R0TypeField> type) {
            Id = id;
            Type = HubBaseV2025R0TypeField.Hubs;
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