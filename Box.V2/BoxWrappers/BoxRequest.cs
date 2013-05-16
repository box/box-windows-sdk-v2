using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Box.V2
{
    public class BoxRequest : IBoxRequest
    {
        public BoxRequest(Uri hostUri, string path)
        {
            Host = hostUri;
            Path = path;

            HttpHeaders = new List<KeyValuePair<string, string>>();
            Parameters = new Dictionary<string, string>();
        }

        public Uri Host { get; private set; }

        public string Path { get; private set; }

        public RequestMethod Method { get; set; }

        public IList<KeyValuePair<string, string>> HttpHeaders { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public Dictionary<string, string> PayloadParameters { get; private set; }

        public Uri Uri { get { return new Uri(Host, Path); } }

        /// <summary>
        /// Returns the full Uri including host, path, and querystring
        /// </summary>
        public Uri AbsoluteUri 
        { 
            get 
            {
                return new Uri(Uri,
                    Parameters.Count == 0 ? string.Empty :
                    string.Format("?{0}", GetQueryString())); 
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
                                .Select(p => string.Format("{0}={1}", p.Key, p.Value));

            return string.Join("&", paramStrings);
        }
    }

    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
