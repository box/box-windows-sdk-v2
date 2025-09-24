using System.Net;
using System.Net.Http.Headers;

namespace Box.V2
{
    /// <summary>
    /// Interface for all Box responses
    /// </summary>
    /// <typeparam name="T">The return type of the Box response</typeparam>
    public interface IBoxResponse<T> where T : class
    {
        /// <summary>
        /// The response object from a successful response
        /// </summary>
        T ResponseObject { get; set; }

        /// <summary>
        /// The full response string from the request
        /// </summary>
        string ContentString { get; set; }

        /// <summary>
        /// Status of the response
        /// </summary>
        ResponseStatus Status { get; set; }

        /// <summary>
        /// Status code of the HTTP response
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The error associated with an Error status
        /// This will be null in all other cases
        /// </summary>
        BoxError Error { get; set; }

        /// <summary>
        /// Headers returned as part of the response
        /// </summary>
        HttpResponseHeaders Headers { get; set; }
    }
}
