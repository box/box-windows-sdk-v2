using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class LegalHoldPolicyAssignmentFilterField : ISerializable {
        /// <summary>
        /// The ID of the metadata template field to filter on.
        /// </summary>
        [JsonPropertyName("field")]
        public string Field { get; set; }

        /// <summary>
        /// The value of the metadata template field to filter on.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public LegalHoldPolicyAssignmentFilterField(string field, string value) {
            Field = field;
            Value = value;
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