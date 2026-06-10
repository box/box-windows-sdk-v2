using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class AiOptionsRules : ISerializable {
        /// <summary>
        /// Indicates whether the field is a multi-select field.
        /// If true, the field can have multiple values.
        /// </summary>
        [JsonPropertyName("multi_select")]
        public bool? MultiSelect { get; set; }

        /// <summary>
        /// The selectable levels for the field.
        /// This is used to limit the levels of the taxonomy that can be selected.
        /// </summary>
        [JsonPropertyName("selectable_levels")]
        public IReadOnlyList<long> SelectableLevels { get; set; }

        public AiOptionsRules() {
            
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