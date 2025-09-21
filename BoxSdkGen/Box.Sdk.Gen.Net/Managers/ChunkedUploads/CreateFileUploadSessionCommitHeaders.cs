using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateFileUploadSessionCommitHeaders {
        /// <summary>
        /// The [RFC3230][1] message digest of the whole file.
        /// 
        /// Only SHA1 is supported. The SHA1 digest must be Base64
        /// encoded. The format of this header is as
        /// `sha=BASE64_ENCODED_DIGEST`.
        /// 
        /// [1]: https://tools.ietf.org/html/rfc3230
        /// </summary>
        public string Digest { get; }

        /// <summary>
        /// Ensures this item hasn't recently changed before
        /// making changes.
        /// 
        /// Pass in the item's last observed `etag` value
        /// into this header and the endpoint will fail
        /// with a `412 Precondition Failed` if it
        /// has changed since.
        /// </summary>
        public string? IfMatch { get; init; }

        /// <summary>
        /// Ensures an item is only returned if it has changed.
        /// 
        /// Pass in the item's last observed `etag` value
        /// into this header and the endpoint will fail
        /// with a `304 Not Modified` if the item has not
        /// changed since.
        /// </summary>
        public string? IfNoneMatch { get; init; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string?> ExtraHeaders { get; }

        public CreateFileUploadSessionCommitHeaders(string digest, Dictionary<string, string?>? extraHeaders = default) {
            Digest = digest;
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string?>() {  };
        }
    }
}