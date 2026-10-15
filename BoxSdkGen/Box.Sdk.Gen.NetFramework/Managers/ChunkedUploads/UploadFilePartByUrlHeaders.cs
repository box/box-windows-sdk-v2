using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UploadFilePartByUrlHeaders {
        /// <summary>
        /// The [RFC3230][1] message digest of the chunk uploaded.
        /// 
        /// `SHA-1` and `SHA-512` ([RFC5843][2]) are supported. The digest must be base64
        /// encoded. The format of this header is as
        /// `sha=BASE64_ENCODED_DIGEST` for `SHA-1`, or
        /// `sha-512=BASE64_ENCODED_DIGEST` for `SHA-512`.
        /// 
        /// To get the value for the digest, use the
        /// openSSL command to encode the file part:
        /// `openssl sha1 -binary <FILE_PART_NAME> | base64` for `SHA-1`, or
        /// `openssl sha512 -binary <FILE_PART_NAME> | base64` for `SHA-512`.
        /// 
        /// [1]: https://tools.ietf.org/html/rfc3230
        /// [2]: https://tools.ietf.org/html/rfc5843
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        /// The byte range of the chunk.
        /// 
        /// Must not overlap with the range of a part already
        /// uploaded this session. Each part’s size must be
        /// exactly equal in size to the part size specified
        /// in the upload session that you created.
        /// One exception is the last part of the file, as this can be smaller.
        /// 
        /// When providing the value for `content-range`, remember that:
        /// 
        /// * The lower bound of each part's byte range
        ///   must be a multiple of the part size.
        /// * The higher bound must be a multiple of the part size - 1.
        /// </summary>
        public string ContentRange { get; set; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string> ExtraHeaders { get; set; }

        public UploadFilePartByUrlHeaders(string digest, string contentRange, Dictionary<string, string> extraHeaders = default) {
            Digest = digest;
            ContentRange = contentRange;
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string>() {  };
        }
    }
}