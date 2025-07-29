using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateClassificationRequestBodyDataField : ISerializable {
        /// <summary>
        /// A new label for the classification, as it will be
        /// shown in the web and mobile interfaces.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; }

        /// <summary>
        /// A static configuration for the classification.
        /// </summary>
        [JsonPropertyName("staticConfig")]
        public UpdateClassificationRequestBodyDataStaticConfigField? StaticConfig { get; init; }

        public UpdateClassificationRequestBodyDataField(string key) {
            Key = key;
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