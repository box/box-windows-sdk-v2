using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleMappingErrorV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier for the mapping error.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `shuttle_mapping_error`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleMappingErrorV2026R0TypeField>))]
        public StringEnum<ShuttleMappingErrorV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The unique identifier of the shuttle job this error belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; set; }

        /// <summary>
        /// The mapping revision this error belongs to.
        /// </summary>
        [JsonPropertyName("revision")]
        public int Revision { get; set; }

        /// <summary>
        /// The category of mapping work that produced the error. This is an open
        /// string whose set of values can grow.
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// The path or subject that the mapping error refers to.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        /// Human-readable messages describing the mapping error.
        /// </summary>
        [JsonPropertyName("messages")]
        public IReadOnlyList<string> Messages { get; set; }

        public ShuttleMappingErrorV2026R0(string id, string jobId, int revision, string category, string path, IReadOnlyList<string> messages, ShuttleMappingErrorV2026R0TypeField type = ShuttleMappingErrorV2026R0TypeField.ShuttleMappingError) {
            Id = id;
            Type = type;
            JobId = jobId;
            Revision = revision;
            Category = category;
            Path = path;
            Messages = messages;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleMappingErrorV2026R0(string id, string jobId, int revision, string category, string path, IReadOnlyList<string> messages, StringEnum<ShuttleMappingErrorV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleMappingErrorV2026R0TypeField.ShuttleMappingError;
            JobId = jobId;
            Revision = revision;
            Category = category;
            Path = path;
            Messages = messages;
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