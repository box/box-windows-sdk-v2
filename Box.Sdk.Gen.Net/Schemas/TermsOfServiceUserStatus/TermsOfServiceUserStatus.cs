using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TermsOfServiceUserStatus : ISerializable {
        /// <summary>
        /// The unique identifier for this terms of service user status.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `terms_of_service_user_status`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TermsOfServiceUserStatusTypeField>))]
        public StringEnum<TermsOfServiceUserStatusTypeField> Type { get; }

        [JsonPropertyName("tos")]
        public TermsOfServiceBase? Tos { get; init; }

        [JsonPropertyName("user")]
        public UserMini? User { get; init; }

        /// <summary>
        /// If the user has accepted the terms of services.
        /// </summary>
        [JsonPropertyName("is_accepted")]
        public bool? IsAccepted { get; init; }

        /// <summary>
        /// When the legal item was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// When the legal item was modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        public TermsOfServiceUserStatus(string id, TermsOfServiceUserStatusTypeField type = TermsOfServiceUserStatusTypeField.TermsOfServiceUserStatus) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal TermsOfServiceUserStatus(string id, StringEnum<TermsOfServiceUserStatusTypeField> type) {
            Id = id;
            Type = TermsOfServiceUserStatusTypeField.TermsOfServiceUserStatus;
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