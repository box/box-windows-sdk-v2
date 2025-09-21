using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class AddClassificationRequestBodyDataField : ISerializable {
        /// <summary>
        /// The label of the classification as shown in the web and
        /// mobile interfaces. This is the only field required to
        /// add a classification.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; }

        /// <summary>
        /// A static configuration for the classification.
        /// </summary>
        [JsonPropertyName("staticConfig")]
        public AddClassificationRequestBodyDataStaticConfigField? StaticConfig { get; init; }

        public AddClassificationRequestBodyDataField(string key) {
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