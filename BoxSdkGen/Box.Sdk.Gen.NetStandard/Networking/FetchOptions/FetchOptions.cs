using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public class FetchOptions {
        /// <summary>
        /// URL of the request
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// HTTP verb of the request
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// HTTP query parameters
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// HTTP headers
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Request body of the request
        /// </summary>
        public SerializedData Data { get; set; }

        /// <summary>
        /// Stream data of the request
        /// </summary>
        public System.IO.Stream FileStream { get; set; }

        /// <summary>
        /// Multipart data of the request
        /// </summary>
        public IReadOnlyList<MultipartItem> MultipartData { get; set; }

        /// <summary>
        /// Content type of the request body
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Expected response format
        /// </summary>
        public Box.Sdk.Gen.ResponseFormat ResponseFormat { get; set; }

        /// <summary>
        /// Authentication object
        /// </summary>
        public IAuthentication Auth { get; set; }

        /// <summary>
        /// Network session object
        /// </summary>
        public NetworkSession NetworkSession { get; set; }

        /// <summary>
        /// Cancellation token
        /// </summary>
        public System.Threading.CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// A boolean value indicate if the request should follow redirects. Defaults to True. Not supported in Browser environment.
        /// </summary>
        public bool? FollowRedirects { get; set; } = true;

        public FetchOptions(string url, string method, string contentType = "application/json", Box.Sdk.Gen.ResponseFormat responseFormat = Box.Sdk.Gen.ResponseFormat.Json) {
            Url = url;
            Method = method;
            ContentType = contentType;
            ResponseFormat = responseFormat;
        }
    }
}