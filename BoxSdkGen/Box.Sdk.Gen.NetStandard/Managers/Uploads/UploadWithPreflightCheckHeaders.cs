using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UploadWithPreflightCheckHeaders {
        /// <summary>
        /// An optional header containing the SHA1 hash of the file to
        /// ensure that the file was not corrupted in transit.
        /// </summary>
        public string ContentMd5 { get; set; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string> ExtraHeaders { get; set; }

        public UploadWithPreflightCheckHeaders(Dictionary<string, string> extraHeaders = default) {
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string>() {  };
        }
    }
}