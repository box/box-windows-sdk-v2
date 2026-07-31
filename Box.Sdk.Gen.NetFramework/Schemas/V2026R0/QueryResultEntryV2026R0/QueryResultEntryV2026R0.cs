using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class QueryResultEntryV2026R0 : IJsonOnDeserialized, ISerializable {
        /// <summary>
        /// The unique identifier of the matching item.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of the matching item.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("extraData")]
        public Dictionary<string, object> ExtraData { get; set; }

        public QueryResultEntryV2026R0(string id, string type) {
            Id = id;
            Type = type;
        }
        /// <summary>
        /// Field only for SDK usage. Use ExtraData field instead. Stores additional fields returned from the api that are not mapped to the other members of this class.
        /// </summary>
        [JsonExtensionData]
        [JsonInclude]
        internal Dictionary<string, JsonElement> _additionalProperties { get; private set; } = default;

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