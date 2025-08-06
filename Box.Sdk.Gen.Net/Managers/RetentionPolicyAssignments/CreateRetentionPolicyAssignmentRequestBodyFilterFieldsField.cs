using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateRetentionPolicyAssignmentRequestBodyFilterFieldsField : ISerializable {
        /// <summary>
        /// The metadata attribute key id.
        /// </summary>
        [JsonPropertyName("field")]
        public string? Field { get; init; }

        /// <summary>
        /// The metadata attribute field id. For value, only
        /// enum and multiselect types are supported.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; init; }

        public CreateRetentionPolicyAssignmentRequestBodyFilterFieldsField() {
            
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