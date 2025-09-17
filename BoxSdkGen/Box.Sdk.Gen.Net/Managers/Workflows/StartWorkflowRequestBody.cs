using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class StartWorkflowRequestBody : ISerializable {
        /// <summary>
        /// The type of the parameters object.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<StartWorkflowRequestBodyTypeField>))]
        public StringEnum<StartWorkflowRequestBodyTypeField>? Type { get; init; }

        /// <summary>
        /// The flow that will be triggered.
        /// </summary>
        [JsonPropertyName("flow")]
        public StartWorkflowRequestBodyFlowField Flow { get; }

        /// <summary>
        /// The array of files for which the workflow should start. All files
        /// must be in the workflow's configured folder.
        /// </summary>
        [JsonPropertyName("files")]
        public IReadOnlyList<StartWorkflowRequestBodyFilesField> Files { get; }

        /// <summary>
        /// The folder object for which the workflow is configured.
        /// </summary>
        [JsonPropertyName("folder")]
        public StartWorkflowRequestBodyFolderField Folder { get; }

        /// <summary>
        /// A configurable outcome the workflow should complete.
        /// </summary>
        [JsonPropertyName("outcomes")]
        public IReadOnlyList<Outcome>? Outcomes { get; init; }

        public StartWorkflowRequestBody(StartWorkflowRequestBodyFlowField flow, IReadOnlyList<StartWorkflowRequestBodyFilesField> files, StartWorkflowRequestBodyFolderField folder) {
            Flow = flow;
            Files = files;
            Folder = folder;
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