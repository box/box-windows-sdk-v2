using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AutomateWorkflowReferenceV2026R0 : ISerializable {
        /// <summary>
        /// The identifier for the Automate workflow instance.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The object type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AutomateWorkflowReferenceV2026R0TypeField>))]
        public StringEnum<AutomateWorkflowReferenceV2026R0TypeField> Type { get; }

        /// <summary>
        /// The display name for the Automate workflow.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        public AutomateWorkflowReferenceV2026R0(string id, AutomateWorkflowReferenceV2026R0TypeField type = AutomateWorkflowReferenceV2026R0TypeField.Workflow) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AutomateWorkflowReferenceV2026R0(string id, StringEnum<AutomateWorkflowReferenceV2026R0TypeField> type) {
            Id = id;
            Type = AutomateWorkflowReferenceV2026R0TypeField.Workflow;
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