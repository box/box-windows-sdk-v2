
namespace Box.V2
{
    public class BoxResponse<T> : IBoxResponse<T> where T : class
    {
        /// <summary>
        /// The object representation of a successful response
        /// </summary>
        public T ResponseObject { get; set;  }

        /// <summary>
        /// The full response string from the request
        /// </summary>
        public string ContentString { get; set; }

        /// <summary>
        /// Status of the response
        /// </summary>
        public ResponseStatus Status { get; set; }

        /// <summary>
        /// Status code of the HTTP response
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The error associated with an Error status
        /// This will be null in all other cases
        /// </summary>
        public BoxError Error { get; set; }
    }

    public enum ResponseStatus
    {
        Unknown,
        Success,
        Pending,
        Error,
        Unauthorized
    }
}
