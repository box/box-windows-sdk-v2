using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a preflight check response
    /// </summary>
    public class BoxPreflightCheck
    {
        public const string FieldUploadUrl = "upload_url";
        public const string FieldUploadToken = "upload_token";

        /// <summary>
        /// The upload URL to optionally use when uploading the file
        /// </summary>
        [JsonProperty(PropertyName = FieldUploadUrl)]
        public virtual string UploadUrl { get; private set; }

        /// <summary>
        /// Convenience method to create Uri instance from UploadUrl string value
        /// </summary>
        public virtual Uri UploadUri
        {
            get
            {
                return new Uri(UploadUrl);
            }
        }

        /// <summary>
        /// An optional access token to use to upload the file.
        /// </summary>
        [JsonProperty(PropertyName = FieldUploadToken)]
        public virtual string UploadToken { get; private set; }

        /// <summary>
        /// True if the upload would be successful;
        /// </summary>
        public virtual bool Success { get; set; }
    }
}
