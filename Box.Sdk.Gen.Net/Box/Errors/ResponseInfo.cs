using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen
{
    public class ResponseInfo
    {
        public int StatusCode { get; }

        public IReadOnlyDictionary<string, string> Headers { get; }

        public SerializedData? Body { get; }

        public string? RawBody { get; }

        public string? Code { get; }

        public Dictionary<string, object>? ContextInfo { get; }

        public string? RequestId { get; }

        public string? HelpUrl { get; }

        public ResponseInfo(int statusCode, IReadOnlyDictionary<string, string> headers, SerializedData body, string rawBody,
            string? code, Dictionary<string, object>? contextInfo, string? requestId, string? helpUrl)
        {
            StatusCode = statusCode;
            Headers = headers;
            Body = body;
            RawBody = rawBody;
            Code = code;
            ContextInfo = contextInfo ?? new Dictionary<string, object>();
            RequestId = requestId;
            HelpUrl = helpUrl;
        }

        internal string Print(DataSanitizer dataSanitizer)
        {
            string headersString = Headers.Count > 0
                ? string.Join(", ", dataSanitizer.SanitizeHeaders(new Dictionary<string, string>(Headers)).Select(kvp => $"{kvp.Key}: {kvp.Value}"))
                : "None";

            string contextInfoString = (ContextInfo != null && ContextInfo.Count > 0)
                ? string.Join(", ", ContextInfo.Select(kvp => $"{kvp.Key}: {kvp.Value}"))
                : "None";

            string sanitizedBody = Body != null ? JsonUtils.SdToJson(dataSanitizer.SanitizeBody(Body)) : "None";

            return $"ResponseInfo:\n" +
                   $"\tStatus Code: {StatusCode}\n" +
                   $"\tHeaders: {headersString}\n" +
                   $"\tBody: {sanitizedBody}\n" +
                   $"\tCode: {(string.IsNullOrEmpty(Code) ? "None" : Code)}\n" +
                   $"\tContext Info: {contextInfoString}\n" +
                   $"\tRequest ID: {(string.IsNullOrEmpty(RequestId) ? "None" : RequestId)}\n" +
                   $"\tHelp URL: {(string.IsNullOrEmpty(HelpUrl) ? "None" : HelpUrl)}";
        }

    }

    internal class BoxApiExceptionDetails
    {
        [JsonPropertyName("code")]
        public string? Code { get; init; }

        [JsonPropertyName("context_info")]
        public Dictionary<string, object>? ContextInfo { get; init; }

        [JsonPropertyName("request_id")]
        public string? RequestId { get; init; }

        [JsonPropertyName("help_url")]
        public string? HelpUrl { get; init; }

        public BoxApiExceptionDetails() { }
    }

}