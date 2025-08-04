using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class LegalHoldPolicyAssignment : LegalHoldPolicyAssignmentBase, ISerializable {
        [JsonPropertyName("legal_hold_policy")]
        public LegalHoldPolicyMini? LegalHoldPolicy { get; init; }

        [JsonPropertyName("assigned_to")]
        public FileOrFolderOrWebLink? AssignedTo { get; init; }

        [JsonPropertyName("assigned_by")]
        public UserMini? AssignedBy { get; init; }

        /// <summary>
        /// When the legal hold policy assignment object was
        /// created.
        /// </summary>
        [JsonPropertyName("assigned_at")]
        public System.DateTimeOffset? AssignedAt { get; init; }

        /// <summary>
        /// When the assignment release request was sent.
        /// (Because it can take time for an assignment to fully
        /// delete, this isn't quite the same time that the
        /// assignment is fully deleted). If null, Assignment
        /// was not deleted.
        /// </summary>
        [JsonPropertyName("deleted_at")]
        public System.DateTimeOffset? DeletedAt { get; init; }

        public LegalHoldPolicyAssignment() {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}