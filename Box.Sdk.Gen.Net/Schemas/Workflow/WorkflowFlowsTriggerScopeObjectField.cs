using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WorkflowFlowsTriggerScopeObjectField : ISerializable {
        /// <summary>
        /// The type of the object.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsTriggerScopeObjectTypeField>))]
        public StringEnum<WorkflowFlowsTriggerScopeObjectTypeField>? Type { get; init; }

        /// <summary>
        /// The id of the object.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public WorkflowFlowsTriggerScopeObjectField() {
            
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