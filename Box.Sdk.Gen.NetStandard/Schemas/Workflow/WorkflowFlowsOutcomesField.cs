using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WorkflowFlowsOutcomesField : ISerializable {
        /// <summary>
        /// The identifier of the outcome.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The outcomes resource type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsOutcomesTypeField>))]
        public StringEnum<WorkflowFlowsOutcomesTypeField> Type { get; set; }

        /// <summary>
        /// The name of the outcome.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("action_type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsOutcomesActionTypeField>))]
        public StringEnum<WorkflowFlowsOutcomesActionTypeField> ActionType { get; set; }

        /// <summary>
        /// If `action_type` is `assign_task` and the task is rejected, returns a
        /// list of outcomes to complete.
        /// </summary>
        [JsonPropertyName("if_rejected")]
        public IReadOnlyList<WorkflowFlowsOutcomesIfRejectedField> IfRejected { get; set; }

        public WorkflowFlowsOutcomesField() {
            
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