using System;
using System.Collections.Generic;
using System.Text;

namespace Box.V2
{
    /// <summary>
    /// Interface for all Box requests
    /// </summary>
    public interface IBoxRequest
    {
        /// <summary>
        /// The host URI
        /// </summary>
        Uri Host { get; }

        /// <summary>
        /// The endpoint path
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Dictionary of headers
        /// </summary>
        Dictionary<string, string> HttpHeaders { get; }

        /// <summary>
        /// Dictionary of querystring parameters
        /// </summary>
        Dictionary<string, string> Parameters { get; }

        /// <summary>
        /// Dictionary of parameters to be included in the request payload
        /// </summary>
        Dictionary<string, string> PayloadParameters { get; }

        /// <summary>
        /// The Content Type of the Payload. The Content Type must be set here and not as part of the Header
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// The Encoding for the Payload
        /// </summary>
        Encoding ContentEncoding { get; set; }

        /// <summary>
        /// The string payload to be included in the body of a request
        /// </summary>
        string Payload { get; set; }

        /// <summary>
        /// The authorization to be used for the request
        /// </summary>
        string Authorization { get; set; }

        /// <summary>
        /// The type of request method
        /// </summary>
        RequestMethod Method { get; set; }

        /// <summary>
        /// Gets the full URI including the host, path and querystring parameters
        /// </summary>
        Uri AbsoluteUri { get; }

        /// <summary>
        /// Gets the URI including just the host and path
        /// </summary>
        Uri Uri { get; }

        /// <summary>
        /// Gets or Sets the timeout value used for the request
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets or Sets whether or not to follow an HTTP redirect
        /// </summary>
        bool FollowRedirect { get; set; }

        /// <summary>
        /// Gets just the query string
        /// </summary>
        /// <returns></returns>
        string GetQueryString();
    }
}
