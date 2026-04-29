using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AutomateWorkflowActionV2026R0 : ISerializable {
        /// <summary>
        /// The identifier for the Automate action.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The object type for this workflow action wrapper.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AutomateWorkflowActionV2026R0TypeField>))]
        public StringEnum<AutomateWorkflowActionV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The type that defines the behavior of this action.
        /// </summary>
        [JsonPropertyName("action_type")]
        [JsonConverter(typeof(StringEnumConverter<AutomateWorkflowActionV2026R0ActionTypeField>))]
        public StringEnum<AutomateWorkflowActionV2026R0ActionTypeField> ActionType { get; set; }

        /// <summary>
        /// A human-readable description of the workflow action.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The date and time when the action was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the action was last updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserMiniV2026R0 CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public UserMiniV2026R0 UpdatedBy { get; set; }

        [JsonPropertyName("workflow")]
        public AutomateWorkflowReferenceV2026R0 Workflow { get; set; }

        public AutomateWorkflowActionV2026R0(string id, AutomateWorkflowReferenceV2026R0 workflow, AutomateWorkflowActionV2026R0TypeField type = AutomateWorkflowActionV2026R0TypeField.WorkflowAction, AutomateWorkflowActionV2026R0ActionTypeField actionType = AutomateWorkflowActionV2026R0ActionTypeField.RunWorkflow) {
            Id = id;
            Type = type;
            ActionType = actionType;
            Workflow = workflow;
        }
        
        [JsonConstructorAttribute]
        internal AutomateWorkflowActionV2026R0(string id, AutomateWorkflowReferenceV2026R0 workflow, StringEnum<AutomateWorkflowActionV2026R0TypeField> type, StringEnum<AutomateWorkflowActionV2026R0ActionTypeField> actionType) {
            Id = id;
            Type = AutomateWorkflowActionV2026R0TypeField.WorkflowAction;
            ActionType = AutomateWorkflowActionV2026R0ActionTypeField.RunWorkflow;
            Workflow = workflow;
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