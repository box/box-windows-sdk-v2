using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WorkflowFlowsField : ISerializable {
        /// <summary>
        /// The identifier of the flow.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The flow's resource type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsTypeField>))]
        public StringEnum<WorkflowFlowsTypeField> Type { get; set; }

        [JsonPropertyName("trigger")]
        public WorkflowFlowsTriggerField Trigger { get; set; }

        [JsonPropertyName("outcomes")]
        public IReadOnlyList<WorkflowFlowsOutcomesField> Outcomes { get; set; }

        /// <summary>
        /// When this flow was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserBase CreatedBy { get; set; }

        public WorkflowFlowsField() {
            
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