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
        public const string FieldUploadToken = "upload_token";

        /// <summary>
        /// The upload URL to optionally use when uploading the file (e.g. when upload acceleration is enabled for your Enterprise).
        /// </summary>
        [JsonProperty(PropertyName = FieldUploadUrl)]
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
        public string UploadToken { get; private set; }
    }
}
