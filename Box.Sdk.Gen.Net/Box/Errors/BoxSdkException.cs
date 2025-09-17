using System;

namespace Box.Sdk.Gen
{
    public class BoxSdkException : Exception
    {
        public System.DateTimeOffset? Timestamp { get; }

        public string? Error { get; }

        public string Name { get; }

        public BoxSdkException(string message, DateTimeOffset? timeStamp = null, string name = "BoxSdkException") : base(message)
        {
            Name = name;
            Timestamp = timeStamp ?? DateTimeOffset.UtcNow;
        }
    }
}