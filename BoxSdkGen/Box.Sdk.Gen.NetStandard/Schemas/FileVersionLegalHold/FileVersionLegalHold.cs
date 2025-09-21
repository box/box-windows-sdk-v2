using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileVersionLegalHold : ISerializable {
        /// <summary>
        /// The unique identifier for this file version legal hold.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `file_version_legal_hold`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileVersionLegalHoldTypeField>))]
        public StringEnum<FileVersionLegalHoldTypeField> Type { get; set; }

        [JsonPropertyName("file_version")]
        public FileVersionMini FileVersion { get; set; }

        [JsonPropertyName("file")]
        public FileMini File { get; set; }

        /// <summary>
        /// List of assignments contributing to this Hold.
        /// </summary>
        [JsonPropertyName("legal_hold_policy_assignments")]
        public IReadOnlyList<LegalHoldPolicyAssignment> LegalHoldPolicyAssignments { get; set; }

        /// <summary>
        /// Time that this File-Version-Legal-Hold was
        /// deleted.
        /// </summary>
        [JsonPropertyName("deleted_at")]
        public System.DateTimeOffset? DeletedAt { get; set; }

        public FileVersionLegalHold() {
            
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