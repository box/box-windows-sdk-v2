using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobCreateRequestV2026R0 : ISerializable {
        /// <summary>
        /// The human-readable name of the shuttle job.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// The type of work the shuttle job performs.
        /// </summary>
        [JsonPropertyName("job_type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobCreateRequestV2026R0JobTypeField>))]
        public StringEnum<ShuttleJobCreateRequestV2026R0JobTypeField> JobType { get; }

        /// <summary>
        /// The unique identifier of the source system to migrate from.
        /// </summary>
        [JsonPropertyName("source_system_id")]
        public string SourceSystemId { get; }

        /// <summary>
        /// An optional free-form comment describing the shuttle job.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; init; }

        /// <summary>
        /// A list of free-form tags associated with the shuttle job.
        /// </summary>
        [JsonPropertyName("tags")]
        public IReadOnlyList<string>? Tags { get; init; }

        /// <summary>
        /// The account mapping configuration that controls how source identities
        /// and permissions are translated into Box. When this object is omitted,
        /// the job is created with `is_enabled` set to `false`.
        /// </summary>
        [JsonPropertyName("account_mapping")]
        public ShuttleJobCreateRequestV2026R0AccountMappingField? AccountMapping { get; init; }

        /// <summary>
        /// The tunable settings that control how the shuttle job runs.
        /// </summary>
        [JsonPropertyName("settings")]
        public ShuttleJobCreateRequestV2026R0SettingsField? Settings { get; init; }

        public ShuttleJobCreateRequestV2026R0(string name, ShuttleJobCreateRequestV2026R0JobTypeField jobType, string sourceSystemId) {
            Name = name;
            JobType = jobType;
            SourceSystemId = sourceSystemId;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleJobCreateRequestV2026R0(string name, StringEnum<ShuttleJobCreateRequestV2026R0JobTypeField> jobType, string sourceSystemId) {
            Name = name;
            JobType = jobType;
            SourceSystemId = sourceSystemId;
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