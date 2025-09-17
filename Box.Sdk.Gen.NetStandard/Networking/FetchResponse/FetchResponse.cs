using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public class FetchResponse {
        /// <summary>
        /// URL of the response
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// HTTP status code of the response
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Response body of the response
        /// </summary>
        public SerializedData Data { get; set; }

        /// <summary>
        /// Streamed content of the response
        /// </summary>
        public System.IO.Stream Content { get; set; }

        /// <summary>
        /// HTTP headers of the response
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        public FetchResponse(int status, Dictionary<string, string> headers) {
            Status = status;
            Headers = headers;
        }
    }
}