using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataTemplateFieldsOptionsRulesField : ISerializable {
        /// <summary>
        /// Whether to allow users to select multiple values.
        /// </summary>
        [JsonPropertyName("multiSelect")]
        public bool? MultiSelect { get; init; }

        /// <summary>
        /// An array of integers defining which levels of the taxonomy are
        /// selectable by users.
        /// </summary>
        [JsonPropertyName("selectableLevels")]
        public IReadOnlyList<long>? SelectableLevels { get; init; }

        public MetadataTemplateFieldsOptionsRulesField() {
            
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