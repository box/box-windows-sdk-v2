using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class WebhookMini : ISerializable {
        /// <summary>
        /// The unique identifier for this webhook.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `webhook`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WebhookMiniTypeField>))]
        public StringEnum<WebhookMiniTypeField>? Type { get; init; }

        /// <summary>
        /// The item that will trigger the webhook.
        /// </summary>
        [JsonPropertyName("target")]
        public WebhookMiniTargetField? Target { get; init; }

        public WebhookMini() {
            
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