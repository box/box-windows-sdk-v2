using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class FileRequestUpdateRequest : ISerializable {
        /// <summary>
        /// An optional new title for the file request. This can be
        /// used to change the title of the file request.
        /// 
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        /// An optional new description for the file request. This can be
        /// used to change the description of the file request.
        /// 
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// An optional new status of the file request.
        /// 
        /// When the status is set to `inactive`, the file request
        /// will no longer accept new submissions, and any visitor
        /// to the file request URL will receive a `HTTP 404` status
        /// code.
        /// 
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<FileRequestUpdateRequestStatusField>))]
        public StringEnum<FileRequestUpdateRequestStatusField>? Status { get; init; }

        /// <summary>
        /// Whether a file request submitter is required to provide
        /// their email address.
        /// 
        /// When this setting is set to true, the Box UI will show
        /// an email field on the file request form.
        /// 
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonPropertyName("is_email_required")]
        public bool? IsEmailRequired { get; init; }

        /// <summary>
        /// Whether a file request submitter is required to provide
        /// a description of the files they are submitting.
        /// 
        /// When this setting is set to true, the Box UI will show
        /// a description field on the file request form.
        /// 
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonPropertyName("is_description_required")]
        public bool? IsDescriptionRequired { get; init; }

        /// <summary>
        /// The date after which a file request will no longer accept new
        /// submissions.
        /// 
        /// After this date, the `status` will automatically be set to
        /// `inactive`.
        /// 
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset? ExpiresAt { get; init; }

        public FileRequestUpdateRequest() {
            
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