using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// A standard representation of a file request, as returned from any file request API endpoints by default.
    /// </summary>
    public class BoxFileRequestObject : BoxEntity
    {
        public const string FieldCreatedAt = "created_at";
        public const string FieldCreatedBy = "created_by";
        public const string FieldDescription = "description";
        public const string FieldEtag = "etag";
        public const string FieldExpiresAt = "expires_at";
        public const string FieldFolder = "folder";
        public const string FieldIsDescriptionRequired = "is_description_required";
        public const string FieldIsEmailRequired = "is_email_required";
        public const string FieldStatus = "status";
        public const string FieldTitle = "title";
        public const string FieldUpdatedAt = "updated_at";
        public const string FieldUpdatedBy = "updated_by";
        public const string FieldUrl = "url";

        /// <summary>
        /// The date and time when the file request was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The user who created this file request.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The optional description of this file request. This is shown in the Box UI to users uploading files. This defaults to description of the file request that was copied to create this file request.
        /// </summary>
        [JsonProperty(PropertyName = FieldDescription)]
        public virtual string Description { get; private set; }

        /// <summary>
        /// The HTTP etag of this file. This can be used in combination with the If-Match header when updating a file request.
        /// By providing that header, a change will only be performed on the file request if the etag on the file request still matches the etag provided in the If-Match header.
        /// /// </summary>
        [JsonProperty(PropertyName = FieldEtag)]
        public virtual string Etag { get; private set; }

        /// <summary>
        /// The date after which a file request will no longer accept new submissions.
        /// After this date, the status will automatically be set to inactive.
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public virtual DateTimeOffset? ExpiresAt { get; private set; }

        /// <summary>
        /// The folder that this file request is associated with. Files submitted through the file request form will be uploaded to this folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolder)]
        public virtual BoxFolder Folder { get; private set; }

        /// <summary>
        /// Whether a file request submitter is required to provide a description of the files they are submitting.
        /// When this setting is set to true, the Box UI will show a description field on the file request form.
        /// This defaults to setting of file request that was copied to create this file request.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsDescriptionRequired)]
        public virtual bool IsDescriptionRequired { get; private set; }

        /// <summary>
        /// Whether a file request submitter is required to provide their email address.
        /// When this setting is set to true, the Box UI will show an email field on the file request form.
        /// This defaults to setting of file request that was copied to create this file request.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsEmailRequired)]
        public virtual bool IsEmailRequired { get; private set; }

        /// <summary>
        /// Describes the status of the sign request.
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual BoxFileRequestStatus Status { get; private set; }

        /// <summary>
        /// The title of file request.This is shown in the Box UI to users uploading files.
        /// This defaults to title of the file request that was copied to create this file request.
        /// </summary>
        [JsonProperty(PropertyName = FieldTitle)]
        public virtual string Title { get; private set; }

        /// <summary>
        /// The date and time when the file request was last updated.
        /// </summary>
        [JsonProperty(PropertyName = FieldUpdatedAt)]
        public virtual DateTimeOffset? UpdatedAt { get; private set; }

        /// <summary>
        /// The user who last modified this file request.
        /// </summary>
        [JsonProperty(PropertyName = FieldUpdatedBy)]
        public virtual BoxUser UpdatedBy { get; private set; }

        /// <summary>
        /// The generated URL for this file request. This URL can be shared with users to let them upload files to the associated folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldUrl)]
        public virtual string Url { get; private set; }
    }

    public enum BoxFileRequestStatus
    {
        active,
        inactive
    }
}
