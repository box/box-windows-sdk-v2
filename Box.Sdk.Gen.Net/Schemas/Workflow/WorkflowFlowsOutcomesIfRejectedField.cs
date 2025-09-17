using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WorkflowFlowsOutcomesIfRejectedField : ISerializable {
        /// <summary>
        /// The identifier of the outcome.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The outcomes resource type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsOutcomesIfRejectedTypeField>))]
        public StringEnum<WorkflowFlowsOutcomesIfRejectedTypeField>? Type { get; init; }

        /// <summary>
        /// The name of the outcome.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("action_type")]
        [JsonConverter(typeof(StringEnumConverter<WorkflowFlowsOutcomesIfRejectedActionTypeField>))]
        public StringEnum<WorkflowFlowsOutcomesIfRejectedActionTypeField>? ActionType { get; init; }

        public WorkflowFlowsOutcomesIfRejectedField() {
            
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