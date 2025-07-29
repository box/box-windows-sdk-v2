using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SkillInvocationStatusField : ISerializable {
        /// <summary>
        /// The state of this event.
        /// 
        /// * `invoked` - Triggered the skill with event details to start
        ///   applying skill on the file.
        /// * `processing` - Currently processing.
        /// * `success` - Completed processing with a success.
        /// * `transient_failure` - Encountered an issue which can be
        ///   retried.
        /// * `permanent_failure` -  Encountered a permanent issue and
        ///   retry would not help.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter<SkillInvocationStatusStateField>))]
        public StringEnum<SkillInvocationStatusStateField>? State { get; init; }

        /// <summary>
        /// Status information.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; init; }

        /// <summary>
        /// Error code information, if error occurred.
        /// </summary>
        [JsonPropertyName("error_code")]
        public string? ErrorCode { get; init; }

        /// <summary>
        /// Additional status information.
        /// </summary>
        [JsonPropertyName("additional_info")]
        public string? AdditionalInfo { get; init; }

        public SkillInvocationStatusField() {
            
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