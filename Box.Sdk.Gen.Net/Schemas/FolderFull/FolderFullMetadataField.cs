using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Collections.ObjectModel;
using System.Text.Json;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FolderFullMetadataField : IJsonOnDeserialized, ISerializable {
        [JsonPropertyName("extraData")]
        public Dictionary<string, Dictionary<string, MetadataFull>>? ExtraData { get; set; }

        public FolderFullMetadataField() {
            
        }
        /// <summary>
        /// Field only for SDK usage. Use ExtraData field instead. Stores additional fields returned from the api that are not mapped to the other members of this class.
        /// </summary>
        [JsonExtensionData]
        [JsonInclude]
        internal Dictionary<string, JsonElement>? _additionalProperties { get; private set; } = default;

        public void OnDeserialized() {
            if (_additionalProperties != null) {
                ExtraData = new Dictionary<string, Dictionary<string, MetadataFull>>();
                foreach (var kvp in _additionalProperties) {
                    var value = JsonSerializer.Deserialize<Dictionary<string, MetadataFull>>(kvp.Value);
                    if (value != null) {
                        ExtraData.Add(kvp.Key, value);
                    }
                }
                _additionalProperties.Clear();
            }
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