using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataFull : Metadata, IJsonOnDeserialized, ISerializable {
        /// <summary>
        /// Whether the user can edit this metadata instance.
        /// </summary>
        [JsonPropertyName("$canEdit")]
        public bool? CanEdit { get; init; }

        /// <summary>
        /// A UUID to identify the metadata instance.
        /// </summary>
        [JsonPropertyName("$id")]
        public string? Id { get; init; }

        /// <summary>
        /// A unique identifier for the "type" of this instance. This is an
        /// internal system property and should not be used by a client
        /// application.
        /// </summary>
        [JsonPropertyName("$type")]
        public string? Type { get; init; }

        /// <summary>
        /// The last-known version of the template of the object. This is an
        /// internal system property and should not be used by a client
        /// application.
        /// </summary>
        [JsonPropertyName("$typeVersion")]
        public long? TypeVersion { get; init; }

        [JsonPropertyName("extraData")]
        public Dictionary<string, object>? ExtraData { get; set; }

        public MetadataFull() {
            
        }
        /// <summary>
        /// Field only for SDK usage. Use ExtraData field instead. Stores additional fields returned from the api that are not mapped to the other members of this class.
        /// </summary>
        [JsonExtensionData]
        [JsonInclude]
        internal Dictionary<string, JsonElement>? _additionalProperties { get; private set; } = default;

        public void OnDeserialized() {
            if (_additionalProperties != null) {
                ExtraData = new Dictionary<string, object>();
                foreach (var kvp in _additionalProperties) {
                    var value = SimpleJsonSerializer.ConvertJsonElement(kvp.Value);
                    if (value != null) {
                        ExtraData.Add(kvp.Key, value);
                    }
                }
                _additionalProperties.Clear();
            }
        }

        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}