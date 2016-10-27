using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a preflight check response
    /// </summary>
    public class BoxPreflightCheck
    {
        public const string FieldUploadUrl = "upload_url";
        [Obsolete]
        public const string FieldUploadToken = "upload_token";

        /// <summary>
        /// The upload URL to optionally use when uploading the file
        /// </summary>
        [JsonProperty(PropertyName = FieldUploadUrl)]
        [Obsolete]
        public string UploadUrl { get; private set; }

        /// <summary>
        /// Convenience method to create Uri instance from UploadUrl string value
        /// </summary>
        public Uri UploadUri
        {
            get
            {
                return new Uri(this.UploadUrl);
            }
        }

        /// <summary>
        /// Currently not used.
        /// </summary>
        [JsonProperty(PropertyName = FieldUploadToken)]
        [Obsolete]
        public string UploadToken { get; private set; }

        /// <summary>
        /// True if the upload would be successful;
        /// </summary>
        public bool Success { get; set; }
    }
}
