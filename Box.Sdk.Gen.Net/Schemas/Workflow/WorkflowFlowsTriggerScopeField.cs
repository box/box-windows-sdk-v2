using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WorkflowFlowsTriggerScopeField : ISerializable {
        /// <summary>
        /// The trigger scope's resource type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsTriggerScopeTypeField>))]
        public StringEnum<WorkflowFlowsTriggerScopeTypeField>? Type { get; init; }

        /// <summary>
        /// Indicates the path of the condition value to check.
        /// </summary>
        [JsonPropertyName("ref")]
        public string? Ref { get; init; }

        /// <summary>
        /// The object the `ref` points to.
        /// </summary>
        [JsonPropertyName("object")]
        public WorkflowFlowsTriggerScopeObjectField? Object { get; init; }

        public WorkflowFlowsTriggerScopeField() {
            
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