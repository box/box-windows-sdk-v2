using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateTaskAssignmentByIdRequestBody : ISerializable {
        /// <summary>
        /// An optional message by the assignee that can be added to the task.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; init; }

        /// <summary>
        /// The state of the task assigned to the user.
        /// 
        /// * For a task with an `action` value of `complete` this can be
        /// `incomplete` or `completed`.
        /// * For a task with an `action` of `review` this can be
        /// `incomplete`, `approved`, or `rejected`.
        /// </summary>
        [JsonPropertyName("resolution_state")]
        [JsonConverter(typeof(StringEnumConverter<UpdateTaskAssignmentByIdRequestBodyResolutionStateField>))]
        public StringEnum<UpdateTaskAssignmentByIdRequestBodyResolutionStateField>? ResolutionState { get; init; }

        public UpdateTaskAssignmentByIdRequestBody() {
            
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