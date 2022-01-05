using System.Net;
using System.Net.Http.Headers;

namespace Box.V2.Exceptions
{
    /// <summary>
    /// An exception to represent that the current access/refresh tokens are in an 
    /// unrecoverable state. This can either be due to the tokens being revoked or expired.
    /// A new session must be created by going through the OAuth workflow again
    /// </summary>
    public class BoxSessionInvalidatedException : BoxAPIException
    {
        /// <summary>
        /// Instantiates a new BoxSessionInvalidatedException with the provided message and error object, status code and response headers
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <param name="statusCode"></param>
        /// <param name="responseHeaders"></param>
        protected internal BoxSessionInvalidatedException(string message, BoxError error, HttpStatusCode statusCode, HttpResponseHeaders responseHeaders)
            : base(message, error, statusCode, responseHeaders)
        {
        }

        protected internal static new BoxSessionInvalidatedException GetResponseException<T>(string message, IBoxResponse<T> response) where T : class
        {
            var error = GetResponseError(response);
            return new BoxSessionInvalidatedException(GetErrorMessage(message, response, error), response.Error, response.StatusCode, response.Headers);
        }
    }
}
