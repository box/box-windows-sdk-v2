using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateTaskAssignmentRequestBodyTaskField : ISerializable {
        /// <summary>
        /// The ID of the task.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of the item to assign.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateTaskAssignmentRequestBodyTaskTypeField>))]
        public StringEnum<CreateTaskAssignmentRequestBodyTaskTypeField> Type { get; }

        public CreateTaskAssignmentRequestBodyTaskField(string id, CreateTaskAssignmentRequestBodyTaskTypeField type = CreateTaskAssignmentRequestBodyTaskTypeField.Task) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal CreateTaskAssignmentRequestBodyTaskField(string id, StringEnum<CreateTaskAssignmentRequestBodyTaskTypeField> type) {
            Id = id;
            Type = CreateTaskAssignmentRequestBodyTaskTypeField.Task;
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