using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WorkflowFlowsTriggerField : ISerializable {
        /// <summary>
        /// The trigger's resource type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsTriggerTypeField>))]
        public StringEnum<WorkflowFlowsTriggerTypeField>? Type { get; init; }

        /// <summary>
        /// The type of trigger selected for this flow.
        /// </summary>
        [JsonPropertyName("trigger_type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsTriggerTriggerTypeField>))]
        public StringEnum<WorkflowFlowsTriggerTriggerTypeField>? TriggerType { get; init; }

        /// <summary>
        /// List of trigger scopes.
        /// </summary>
        [JsonPropertyName("scope")]
        public IReadOnlyList<WorkflowFlowsTriggerScopeField>? Scope { get; init; }

        public WorkflowFlowsTriggerField() {
            
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