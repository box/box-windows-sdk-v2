using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileRequest : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdescriptionSet")]
        protected bool _isDescriptionSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isetagSet")]
        protected bool _isEtagSet { get; set; }

        protected string? _description { get; set; }

        protected string? _etag { get; set; }

        /// <summary>
        /// The unique identifier for this file request.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `file_request`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileRequestTypeField>))]
        public StringEnum<FileRequestTypeField> Type { get; }

        /// <summary>
        /// The title of file request. This is shown
        /// in the Box UI to users uploading files.
        /// 
        /// This defaults to title of the file request that was
        /// copied to create this file request.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        /// The optional description of this file request. This is
        /// shown in the Box UI to users uploading files.
        /// 
        /// This defaults to description of the file request that was
        /// copied to create this file request.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get => _description; init { _description = value; _isDescriptionSet = true; } }

        /// <summary>
        /// The status of the file request. This defaults
        /// to `active`.
        /// 
        /// When the status is set to `inactive`, the file request
        /// will no longer accept new submissions, and any visitor
        /// to the file request URL will receive a `HTTP 404` status
        /// code.
        /// 
        /// This defaults to status of file request that was
        /// copied to create this file request.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<FileRequestStatusField>))]
        public StringEnum<FileRequestStatusField>? Status { get; init; }

        /// <summary>
        /// Whether a file request submitter is required to provide
        /// their email address.
        /// 
        /// When this setting is set to true, the Box UI will show
        /// an email field on the file request form.
        /// 
        /// This defaults to setting of file request that was
        /// copied to create this file request.
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
        /// This defaults to setting of file request that was
        /// copied to create this file request.
        /// </summary>
        [JsonPropertyName("is_description_required")]
        public bool? IsDescriptionRequired { get; init; }

        /// <summary>
        /// The date after which a file request will no longer accept new
        /// submissions.
        /// 
        /// After this date, the `status` will automatically be set to
        /// `inactive`.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset? ExpiresAt { get; init; }

        [JsonPropertyName("folder")]
        public FolderMini Folder { get; }

        /// <summary>
        /// The generated URL for this file request. This URL can be shared
        /// with users to let them upload files to the associated folder.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        /// The HTTP `etag` of this file. This can be used in combination with
        /// the `If-Match` header when updating a file request. By providing that
        /// header, a change will only be performed on the  file request if the `etag`
        /// on the file request still matches the `etag` provided in the `If-Match`
        /// header.
        /// </summary>
        [JsonPropertyName("etag")]
        public string? Etag { get => _etag; init { _etag = value; _isEtagSet = true; } }

        [JsonPropertyName("created_by")]
        public UserMini? CreatedBy { get; init; }

        /// <summary>
        /// The date and time when the file request was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset CreatedAt { get; }

        [JsonPropertyName("updated_by")]
        public UserMini? UpdatedBy { get; init; }

        /// <summary>
        /// The date and time when the file request was last updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset UpdatedAt { get; }

        public FileRequest(string id, FolderMini folder, System.DateTimeOffset createdAt, System.DateTimeOffset updatedAt, FileRequestTypeField type = FileRequestTypeField.FileRequest) {
            Id = id;
            Type = type;
            Folder = folder;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        
        [JsonConstructorAttribute]
        internal FileRequest(string id, FolderMini folder, System.DateTimeOffset createdAt, System.DateTimeOffset updatedAt, StringEnum<FileRequestTypeField> type) {
            Id = id;
            Type = FileRequestTypeField.FileRequest;
            Folder = folder;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
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