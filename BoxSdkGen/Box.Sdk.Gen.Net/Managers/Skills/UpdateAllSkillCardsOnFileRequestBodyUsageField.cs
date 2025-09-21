using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateAllSkillCardsOnFileRequestBodyUsageField : ISerializable {
        /// <summary>
        /// The value will always be `file`.
        /// </summary>
        [JsonPropertyName("unit")]
        public string? Unit { get; init; }

        /// <summary>
        /// Number of resources affected.
        /// </summary>
        [JsonPropertyName("value")]
        public double? Value { get; init; }

        public UpdateAllSkillCardsOnFileRequestBodyUsageField() {
            
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