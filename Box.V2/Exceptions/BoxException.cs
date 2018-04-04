using Box.V2.Converter;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

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

        /// <summary>
        /// Instantiates a new BoxException with the provided message and error object
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public BoxException(string message, BoxError error) : base(message)
        {
            Error = error;
        }

        /// <summary>
        /// Instantiate a Box specific exception from a given HTTP response
        /// </summary>
        /// <param name="message">The message from the SDK about what happened</param>
        /// <param name="response">The HTTP response that generated the exception</param>
        public BoxException(string message, IBoxResponse<object> response) : base(GetErrorMessage(message, response))
        {
            var converter = new BoxJsonConverter();
            StatusCode = response.StatusCode;
            ResponseHeaders = response.Headers;
            if (!string.IsNullOrWhiteSpace(response.ContentString))
            {
                response.Error = converter.Parse<BoxError>(response.ContentString);
            }

            Response = new BoxErrorResponse(response);
        }

        private static string GetErrorMessage(string message, IBoxResponse<object> response)
        {
            var converter = new BoxJsonConverter();
            BoxError error = null;
            if (!string.IsNullOrWhiteSpace(response.ContentString))
            {
                error = converter.Parse<BoxError>(response.ContentString);
            }
            var str = error?.RequestId;
            return string.Format("%s [%s | %s] %s - %s", message, response.StatusCode, error?.RequestId ?? "N/A", error?.Code ?? "N/A", error?.Message ?? "N/A");
        }

        /// <summary>
        /// Http Status code for the response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Error parsed from the message returned by the API
        /// </summary>
        public BoxError Error { get; set; }

        /// <summary>
        /// Response headers returned by the API
        /// </summary>
        public HttpResponseHeaders ResponseHeaders { get; set; }

        /// <summary>
        /// Information about the HTTP response that caused the error
        /// </summary>
        public BoxErrorResponse Response { get; set; }
    }


    public class BoxConflictException<T> : BoxException  
        where T : class
    {
        private BoxConflictError<T> _conflictError;

        public BoxConflictException(string message, BoxConflictError<T> error) : base(message, error) 
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

    public class BoxPreflightCheckConflictException<T> : BoxException
        where T : class
    {
        private BoxPreflightCheckConflictError<T> _conflictError;

        public BoxPreflightCheckConflictException(string message, BoxPreflightCheckConflictError<T> error) : base(message, error)
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

    public class BoxErrorResponse
    {
        /// <summary>
        /// The status code of the response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The headers of the response
        /// </summary>
        public HttpHeaders Headers { get; set; }

        /// <summary>
        /// The response body contents
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Instantiates a new representation of the error response from the raw response object
        /// </summary>
        /// <param name="response">The raw response object</param>
        public BoxErrorResponse(BoxResponse<object> response)
        {
            StatusCode = response.StatusCode;
            Headers = response.Headers;
            Body = response.ContentString;
        }
    }
}

