using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class RetentionPolicyAssignment : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isfilter_fieldsSet")]
        protected bool _isFilterFieldsSet { get; set; }

        protected IReadOnlyList<RetentionPolicyAssignmentFilterFieldsField>? _filterFields { get; set; }

        /// <summary>
        /// The unique identifier for a retention policy assignment.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `retention_policy_assignment`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<RetentionPolicyAssignmentTypeField>))]
        public StringEnum<RetentionPolicyAssignmentTypeField> Type { get; }

        [JsonPropertyName("retention_policy")]
        public RetentionPolicyMini? RetentionPolicy { get; init; }

        /// <summary>
        /// The `type` and `id` of the content that is under
        /// retention. The `type` can either be `folder`
        /// `enterprise`, or `metadata_template`.
        /// </summary>
        [JsonPropertyName("assigned_to")]
        public RetentionPolicyAssignmentAssignedToField? AssignedTo { get; init; }

        /// <summary>
        /// An array of field objects. Values are only returned if the `assigned_to`
        /// type is `metadata_template`. Otherwise, the array is blank.
        /// </summary>
        [JsonPropertyName("filter_fields")]
        public IReadOnlyList<RetentionPolicyAssignmentFilterFieldsField>? FilterFields { get => _filterFields; init { _filterFields = value; _isFilterFieldsSet = true; } }

        [JsonPropertyName("assigned_by")]
        public UserMini? AssignedBy { get; init; }

        /// <summary>
        /// When the retention policy assignment object was
        /// created.
        /// </summary>
        [JsonPropertyName("assigned_at")]
        public System.DateTimeOffset? AssignedAt { get; init; }

        /// <summary>
        /// The date the retention policy assignment begins.
        /// If the `assigned_to` type is `metadata_template`,
        /// this field can be a date field's metadata attribute key id.
        /// </summary>
        [JsonPropertyName("start_date_field")]
        public string? StartDateField { get; init; }

        public RetentionPolicyAssignment(string id, RetentionPolicyAssignmentTypeField type = RetentionPolicyAssignmentTypeField.RetentionPolicyAssignment) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal RetentionPolicyAssignment(string id, StringEnum<RetentionPolicyAssignmentTypeField> type) {
            Id = id;
            Type = RetentionPolicyAssignmentTypeField.RetentionPolicyAssignment;
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