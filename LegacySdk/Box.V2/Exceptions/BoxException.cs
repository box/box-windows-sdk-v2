using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Box.V2.Converter;

namespace Box.V2.Exceptions
{
    public class BoxException : Exception
    {
        /// <summary>
        /// Instantiates a new BoxException
        /// This exception is used when the SDK throws an exception
        /// </summary>
        public BoxException() : base() { }

        /// <summary>
        /// Instantiates a new BoxException with the provided message
        /// </summary>
        /// <param name="message">The message for the exception</param>
        public BoxException(string message) : base(message) { }

        /// <summary>
        /// Instantiates a new BoxException with the provided message and provided inner Exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception to be wrapped</param>
        public BoxException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class BoxCodingException : BoxException
    {
        /// <summary>
        /// Instantiates a new BoxCodingException with the provided message
        /// </summary>
        /// <param name="message">The message for the exception</param>
        public BoxCodingException(string message) : base(message) { }
    }

    public class BoxAPIException : BoxException
    {
        /// <summary>
        /// Instantiates a new BoxAPIException with the provided message and error object, status code and response headers
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <param name="statusCode"></param>
        /// <param name="responseHeaders"></param>
        protected internal BoxAPIException(string message, BoxError error, HttpStatusCode statusCode, HttpResponseHeaders responseHeaders) : base(message)
        {
            Error = error;
            StatusCode = statusCode;
            ResponseHeaders = responseHeaders;
        }

        /// <summary>
        /// Instantiate a Box specific exception from a given HTTP response
        /// </summary>
        /// <param name="message">The message from the SDK about what happened</param>
        /// <param name="response">The HTTP response that generated the exception</param>
        protected internal static BoxAPIException GetResponseException<T>(string message, IBoxResponse<T> response) where T : class
        {
            var error = GetResponseError(response);
            return new BoxAPIException(GetErrorMessage(message, response, error), response.Error ?? error, response.StatusCode, response.Headers);
        }

        protected internal static BoxError GetResponseError<T>(IBoxResponse<T> response) where T : class
        {
            BoxError error = null;
            if (!string.IsNullOrWhiteSpace(response.ContentString))
            {
                var converter = new BoxJsonConverter();

                try
                {
                    error = converter.Parse<BoxError>(response.ContentString);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(string.Format("Unable to parse error message: {0} ({1})", response.ContentString, e.Message));
                }
            }

            return error;
        }

        protected internal static string GetErrorMessage<T>(string message, IBoxResponse<T> response, BoxError error = null) where T : class
        {
            var requestID = error?.RequestId;
            string traceID = null;
            if (response.Headers != null && response.Headers.TryGetValues("BOX-REQUEST-ID", out IEnumerable<string> traceIDHeaders))
            {
                traceID = traceIDHeaders.FirstOrDefault();
            }

            var errorCode = error?.Code ?? error?.Name;
            var errorDescription = error?.Message ?? error?.Description;

            var exceptionMessage = message;
            exceptionMessage += " [" + response.StatusCode;
            if (requestID != null || traceID != null)
            {
                exceptionMessage += string.Format(" | {0}.{1}", requestID, traceID);
            }
            exceptionMessage += "]";
            if (errorCode != null)
            {
                exceptionMessage += " " + errorCode;
            }
            if (errorDescription != null)
            {
                exceptionMessage += " - " + errorDescription;
            }
            return exceptionMessage;
        }

        /// <summary>
        /// Http Status code for the response
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Error parsed from the message returned by the API
        /// </summary>
        public BoxError Error { get; }

        /// <summary>
        /// Response headers returned by the API
        /// </summary>
        public HttpResponseHeaders ResponseHeaders { get; }

        /// <summary>
        /// Error code of the Error returned by the API. Can be empty
        /// </summary>
        public string ErrorCode => Error?.Code ?? Error?.Name ?? string.Empty;

        /// <summary>
        /// Error description of the Error returned by the API. Can be empty
        /// </summary>
        public string ErrorDescription => Error?.Message ?? Error?.Description ?? string.Empty;
    }


    public class BoxConflictException<T> : BoxAPIException
        where T : class
    {
        private readonly BoxConflictError<T> _conflictError;

        protected internal BoxConflictException(string message, BoxConflictError<T> error, HttpStatusCode statusCode, HttpResponseHeaders responseHeaders)
            : base(message, error, statusCode, responseHeaders)
        {
            _conflictError = error;
        }

        public ICollection<T> ConflictingItems
        {
            get
            {
                return _conflictError != null && _conflictError.ContextInfo != null ?
                    _conflictError.ContextInfo.Conflicts :
                    null;
            }
        }
    }

    public class BoxPreflightCheckConflictException<T> : BoxAPIException
        where T : class
    {
        private readonly BoxPreflightCheckConflictError<T> _conflictError;

        protected internal BoxPreflightCheckConflictException(string message, BoxPreflightCheckConflictError<T> error, HttpStatusCode statusCode, HttpResponseHeaders responseHeaders)
                        : base(message, error, statusCode, responseHeaders)
        {
            _conflictError = error;
        }

        public T ConflictingItem
        {
            get
            {
                return _conflictError != null && _conflictError.ContextInfo != null ?
                    _conflictError.ContextInfo.Conflict :
                    null;
            }
        }
    }
}

