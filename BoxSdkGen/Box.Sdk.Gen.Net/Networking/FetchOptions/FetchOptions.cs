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
        public string Url { get; }

        /// <summary>
        /// HTTP verb of the request
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// HTTP query parameters
        /// </summary>
        public Dictionary<string, string>? Parameters { get; init; }

        /// <summary>
        /// HTTP headers
        /// </summary>
        public Dictionary<string, string>? Headers { get; init; }

        /// <summary>
        /// Request body of the request
        /// </summary>
        public SerializedData? Data { get; init; }

        /// <summary>
        /// Stream data of the request
        /// </summary>
        public System.IO.Stream? FileStream { get; init; }

        /// <summary>
        /// Multipart data of the request
        /// </summary>
        public IReadOnlyList<MultipartItem>? MultipartData { get; init; }

        /// <summary>
        /// Content type of the request body
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Expected response format
        /// </summary>
        public Box.Sdk.Gen.ResponseFormat ResponseFormat { get; }

        /// <summary>
        /// Authentication object
        /// </summary>
        public IAuthentication? Auth { get; init; }

        /// <summary>
        /// Network session object
        /// </summary>
        public NetworkSession? NetworkSession { get; init; }

        /// <summary>
        /// Cancellation token
        /// </summary>
        public System.Threading.CancellationToken? CancellationToken { get; init; }

        /// <summary>
        /// A boolean value indicate if the request should follow redirects. Defaults to True. Not supported in Browser environment.
        /// </summary>
        public bool? FollowRedirects { get; init; } = true;

        public FetchOptions(string url, string method, string contentType = "application/json", Box.Sdk.Gen.ResponseFormat responseFormat = Box.Sdk.Gen.ResponseFormat.Json) {
            Url = url;
            Method = method;
            ContentType = contentType;
            ResponseFormat = responseFormat;
        }
    }
}