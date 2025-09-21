using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen
{
    public class RequestInfo
    {
        public string Method { get; }

        public string Url { get; }

        public IReadOnlyDictionary<string, string> QueryParams { get; }

        public IReadOnlyDictionary<string, string> Headers { get; }

        public string? Body { get; init; }

        public RequestInfo(string method, string? url, IReadOnlyDictionary<string, string>? queryParams, IReadOnlyDictionary<string, string> headers)
        {
            Method = method;
            Url = url ?? "";
            QueryParams = queryParams ?? new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
            Headers = headers;
        }

        internal string Print(DataSanitizer dataSanitizer)
        {
            string queryParamsString = QueryParams.Count > 0
                ? string.Join(", ", QueryParams.Select(kvp => $"{kvp.Key}: {kvp.Value}"))
                : "None";

            string headersString = Headers.Count > 0
                ? string.Join(", ", dataSanitizer.SanitizeHeaders(new Dictionary<string, string>(Headers)).Select(kvp => $"{kvp.Key}: {kvp.Value}"))
                : "None";

            return $"RequestInfo:\n" +
                   $"\tMethod: {Method}\n" +
                   $"\tURL: {Url}\n" +
                   $"\tQuery Params: {queryParamsString}\n" +
                   $"\tHeaders: {headersString}\n" +
                   $"\tBody: {(string.IsNullOrEmpty(Body) ? "None" : Body)}";
        }
    }
}