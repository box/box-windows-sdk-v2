using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateMetadataTemplateRequestBodyFieldsOptionsRulesField : ISerializable {
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

        public CreateMetadataTemplateRequestBodyFieldsOptionsRulesField() {
            
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