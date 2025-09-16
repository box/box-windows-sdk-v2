using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateWebhookRequestBody : ISerializable {
        /// <summary>
        /// The item that will trigger the webhook.
        /// </summary>
        [JsonPropertyName("target")]
        public CreateWebhookRequestBodyTargetField Target { get; }

        /// <summary>
        /// The URL that is notified by this webhook.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; }

        /// <summary>
        /// An array of event names that this webhook is
        /// to be triggered for.
        /// </summary>
        [JsonPropertyName("triggers")]
        [JsonConverter(typeof(StringEnumListConverter<CreateWebhookRequestBodyTriggersField>))]
        public IReadOnlyList<StringEnum<CreateWebhookRequestBodyTriggersField>> Triggers { get; }

        public CreateWebhookRequestBody(CreateWebhookRequestBodyTargetField target, string address, IReadOnlyList<StringEnum<CreateWebhookRequestBodyTriggersField>> triggers) {
            Target = target;
            Address = address;
            Triggers = triggers;
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