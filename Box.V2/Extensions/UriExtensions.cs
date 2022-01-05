using System;
using System.Collections.Generic;
using System.Linq;
using Box.V2.Utility;

namespace Box.V2.Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// Appends the given key value pair to query string of the Uri.
        /// </summary>
        /// <param name="uri">Uri to append the query string value.</param>
        /// <param name="name">Name of the query string.</param>
        /// <param name="value">Value of the query string.</param>
        /// <returns>Url with the query string appended/updated.</returns>
        public static Uri AppendQueryString(this Uri uri, string name, string value)
        {
            var uriBuilder = new UriBuilder(uri);
            Dictionary<string, string> queryStringCollection = Helper.ParseQueryString(uri);
            queryStringCollection[name] = value;
            uriBuilder.Query = string.Join("&", queryStringCollection.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            return uriBuilder.Uri;
        }
    }
}
