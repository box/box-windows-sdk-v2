using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// Used to copy FileRequest.
    /// </summary>
    public class BoxFileRequestCopyRequest
    {
        /// <summary>
        /// The optional description of this file request. This is shown in the Box UI to users uploading files.
        /// This defaults to description of the file request that was copied to create this file request.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The date after which a file request will no longer accept new submissions.
        /// After this date, the status will automatically be set to inactive.
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTimeOffset? ExpiresAt { get; set; }

        /// <summary>
        /// The folder to associate the new file request to.
        /// </summary>
        [JsonProperty(PropertyName = "folder")]
        public BoxRequestEntity Folder { get; set; }

        /// <summary>
        /// Whether a file request submitter is required to provide a description of the files they are submitting.
        /// When this setting is set to true, the Box UI will show a description field on the file request form.
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonProperty(PropertyName = "is_description_required")]
        public bool? IsDescriptionRequired { get; set; }

        /// <summary>
        /// Whether a file request submitter is required to provide their email address.
        /// When this setting is set to true, the Box UI will show an email field on the file request form.
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonProperty(PropertyName = "is_email_required")]
        public bool? IsEmailRequired { get; set; }

        /// <summary>
        /// An optional new status of the file request.
        /// When the status is set to inactive, the file request will no longer accept new submissions, and any visitor to the file request URL will receive a HTTP 404 status code.
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxFileRequestStatus? Status { get; set; }

        /// <summary>
        /// An optional new title for the file request.This can be used to change the title of the file request.
        /// This will default to the value on the existing file request.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
