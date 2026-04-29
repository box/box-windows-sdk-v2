using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class AutomateWorkflowStartRequestV2026R0 : ISerializable {
        /// <summary>
        /// The callable action ID used to trigger the selected workflow.
        /// </summary>
        [JsonPropertyName("workflow_action_id")]
        public string WorkflowActionId { get; }

        /// <summary>
        /// The files to process with the selected workflow.
        /// </summary>
        [JsonPropertyName("file_ids")]
        public IReadOnlyList<string> FileIds { get; }

        public AutomateWorkflowStartRequestV2026R0(string workflowActionId, IReadOnlyList<string> fileIds) {
            WorkflowActionId = workflowActionId;
            FileIds = fileIds;
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