using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleDiscoveredSubjectV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier for the discovered subject.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `shuttle_discovered_subject`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleDiscoveredSubjectV2026R0TypeField>))]
        public StringEnum<ShuttleDiscoveredSubjectV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The unique identifier of the shuttle job this subject belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; set; }

        /// <summary>
        /// The scan revision this discovered subject belongs to.
        /// </summary>
        [JsonPropertyName("revision")]
        public int Revision { get; set; }

        [JsonPropertyName("source")]
        public ShuttleSubjectV2026R0 Source { get; set; }

        /// <summary>
        /// A suggested Box target inferred during the scan. `null` when no
        /// suggestion was made.
        /// </summary>
        [JsonPropertyName("suggested_target")]
        public ShuttleTargetSubjectV2026R0 SuggestedTarget { get; set; }

        public ShuttleDiscoveredSubjectV2026R0(string id, string jobId, int revision, ShuttleSubjectV2026R0 source, ShuttleDiscoveredSubjectV2026R0TypeField type = ShuttleDiscoveredSubjectV2026R0TypeField.ShuttleDiscoveredSubject) {
            Id = id;
            Type = type;
            JobId = jobId;
            Revision = revision;
            Source = source;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleDiscoveredSubjectV2026R0(string id, string jobId, int revision, ShuttleSubjectV2026R0 source, StringEnum<ShuttleDiscoveredSubjectV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleDiscoveredSubjectV2026R0TypeField.ShuttleDiscoveredSubject;
            JobId = jobId;
            Revision = revision;
            Source = source;
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