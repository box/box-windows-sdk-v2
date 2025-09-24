using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Box.V2.Exceptions;

namespace Box.V2
{
    public class BoxRequest : IBoxRequest
    {
        /// <summary>
        /// Instantiates a new Box request with the provided host URI
        /// </summary>
        /// <param name="hostUri"></param>
        public BoxRequest(Uri hostUri) : this(hostUri, string.Empty) { }

        /// <summary>
        /// Instantiates a new Box request with the provided host URI and path
        /// </summary>
        /// <param name="hostUri"></param>
        /// <param name="path"></param>
        public BoxRequest(Uri hostUri, string path)
        {
            var pattern = @"\/\.+";
            if (path != null && Regex.IsMatch(path, pattern) == true)
            {
                throw new BoxCodingException($"An invalid path parameter exists in {path}. Relative path parameters cannot be passed.");
            }

            Host = hostUri;
            Path = path;
            HttpHeaders = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
            PayloadParameters = new Dictionary<string, string>();

            // Initialize Defaults
            ContentEncoding = Encoding.UTF8;
            FollowRedirect = true;
        }

        public Uri Host { get; private set; }

        public string Path { get; private set; }

        public virtual RequestMethod Method { get; set; }

        public Dictionary<string, string> HttpHeaders { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public Dictionary<string, string> PayloadParameters { get; private set; }

        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public Uri Uri { get { return new Uri(Host, Path); } }

        public string Payload { get; set; }

        public string Authorization { get; set; }

        public TimeSpan? Timeout { get; set; }

        public bool FollowRedirect { get; set; }

        /// <summary>
        /// Returns the full Uri including host, path, and querystring
        /// </summary>
        public Uri AbsoluteUri
        {
            get
            {
                var existingQuery = Uri.Query;
                var newQuery = string.IsNullOrWhiteSpace(existingQuery)
                    ? Parameters.Count == 0 ? string.Empty : string.Format("?{0}", GetQueryString())
                    : Parameters.Count == 0 ? existingQuery : string.Format("{0}&{1}", existingQuery, GetQueryString());

                return new Uri(Uri, newQuery);
            }
        }

        /// <summary>
        /// Returns the query string of the parameters dictionary
        /// </summary>
        /// <returns></returns>
        public string GetQueryString()
        {
            if (Parameters.Count == 0)
                return string.Empty;

            var paramStrings = Parameters
                                .Where(p => !string.IsNullOrEmpty(p.Value))
                                .Select(p => string.Format("{0}={1}", p.Key, WebUtility.UrlEncode(p.Value)));

            return string.Join("&", paramStrings);
        }
    }

    /// <summary>
    /// The available request types
    /// </summary>
    public enum RequestMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Options
    }
}
