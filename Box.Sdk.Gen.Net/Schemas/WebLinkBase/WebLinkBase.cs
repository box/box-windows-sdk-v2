using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class WebLinkBase : ISerializable {
        /// <summary>
        /// The unique identifier for this web link.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `web_link`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WebLinkBaseTypeField>))]
        public StringEnum<WebLinkBaseTypeField> Type { get; }

        /// <summary>
        /// The entity tag of this web link. Used with `If-Match`
        /// headers.
        /// </summary>
        [JsonPropertyName("etag")]
        public string? Etag { get; init; }

        public WebLinkBase(string id, WebLinkBaseTypeField type = WebLinkBaseTypeField.WebLink) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal WebLinkBase(string id, StringEnum<WebLinkBaseTypeField> type) {
            Id = id;
            Type = WebLinkBaseTypeField.WebLink;
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