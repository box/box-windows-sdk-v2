using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WebhookInvocation : ISerializable {
        /// <summary>
        /// The unique identifier for this webhook invocation.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `webhook_event`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WebhookInvocationTypeField>))]
        public StringEnum<WebhookInvocationTypeField> Type { get; set; }

        [JsonPropertyName("webhook")]
        public Webhook Webhook { get; set; }

        [JsonPropertyName("created_by")]
        public UserMini CreatedBy { get; set; }

        /// <summary>
        /// A timestamp identifying the time that
        /// the webhook event was triggered.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("trigger")]
        [JsonConverter(typeof(StringEnumConverter<WebhookInvocationTriggerField>))]
        public StringEnum<WebhookInvocationTriggerField> Trigger { get; set; }

        [JsonPropertyName("source")]
        public FileOrFolder Source { get; set; }

        public WebhookInvocation() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}