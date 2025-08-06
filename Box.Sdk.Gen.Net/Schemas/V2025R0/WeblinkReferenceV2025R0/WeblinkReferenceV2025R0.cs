using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class WeblinkReferenceV2025R0 : ISerializable {
        /// <summary>
        /// The value will always be `weblink`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WeblinkReferenceV2025R0TypeField>))]
        public StringEnum<WeblinkReferenceV2025R0TypeField> Type { get; }

        /// <summary>
        /// ID of the weblink.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public WeblinkReferenceV2025R0(string id, WeblinkReferenceV2025R0TypeField type = WeblinkReferenceV2025R0TypeField.Weblink) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal WeblinkReferenceV2025R0(string id, StringEnum<WeblinkReferenceV2025R0TypeField> type) {
            Type = WeblinkReferenceV2025R0TypeField.Weblink;
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