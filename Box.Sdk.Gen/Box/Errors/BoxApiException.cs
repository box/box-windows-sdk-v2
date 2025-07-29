using System;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen
{
    public class BoxApiException : BoxSdkException
    {
        public RequestInfo RequestInfo { get; }

        public ResponseInfo ResponseInfo { get; }

        public DataSanitizer DataSanitizer { get; init; } = new DataSanitizer();

        public BoxApiException(string message, DateTimeOffset timeStamp, RequestInfo requestInfo, ResponseInfo responseInfo) : base(message, timeStamp, "BoxApiException")
        {
            RequestInfo = requestInfo;
            ResponseInfo = responseInfo;
        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   $"{RequestInfo.Print(DataSanitizer)}\n" +
                   $"{ResponseInfo.Print(DataSanitizer)}";
        }

    }
}