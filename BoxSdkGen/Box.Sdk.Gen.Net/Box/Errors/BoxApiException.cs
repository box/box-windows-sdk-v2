using System;
using System.Net;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen
{
    public class BoxApiException : BoxSdkException
    {
        public RequestInfo RequestInfo { get; }

        public ResponseInfo ResponseInfo { get; }

        public DataSanitizer DataSanitizer { get; init; } = new DataSanitizer();

        public BoxApiException(string message, DateTimeOffset timeStamp, RequestInfo requestInfo, ResponseInfo responseInfo)
            : base(BuildExceptionMessage(message, responseInfo), timeStamp, "BoxApiException")
        {
            RequestInfo = requestInfo;
            ResponseInfo = responseInfo;
        }

        private static string BuildExceptionMessage(string message, ResponseInfo? responseInfo)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                return message;
            }

            var statusCode = responseInfo?.StatusCode ?? 0;
            var reasonPhrase = Enum.IsDefined(typeof(HttpStatusCode), statusCode)
                ? ((HttpStatusCode)statusCode).ToString()
                : "Unknown";
            return $"The API returned an unexpected response: [{statusCode} {reasonPhrase}]";
        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   $"{RequestInfo.Print(DataSanitizer)}\n" +
                   $"{ResponseInfo.Print(DataSanitizer)}";
        }

    }
}