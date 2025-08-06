using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Box.Sdk.Gen.Internal
{
    static class HttpUtils
    {
        internal static Uri BuildUri(string resource, IReadOnlyDictionary<string, string>? parameters)
        {
            return parameters?.Count > 0 ?
                new Uri(string.Join("?", resource, string.Join('&',
                    parameters.Select(q => $"{HttpUtility.UrlEncode(q.Key)}={HttpUtility.UrlEncode(q.Value)}")))) :
                new Uri(resource);
        }
    }
}
