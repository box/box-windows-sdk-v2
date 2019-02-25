using Box.V2.Converter;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

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
        public static BoxException GetResponseException<T>(string message, IBoxResponse<T> response) where T : class
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
            var ex = new BoxException(GetErrorMessage(message, response, error))
            {
                StatusCode = response.StatusCode,
                ResponseHeaders = response.Headers,
                Error = error
            };

            return ex;
        }

        private static string GetErrorMessage<T>(string message, IBoxResponse<T> response, BoxError error = null) where T : class
        {
            var requestID = error?.RequestId != null ? string.Format(" | {0}", error.RequestId) : "";
            IEnumerable<string> traceIDHeaders;
            if (response.Headers.TryGetValues("BOX-REQUEST-ID", out traceIDHeaders))
            {
                foreach (var id in traceIDHeaders)
                {
                    // Take the first trace ID header value (there should only be one)
                    requestID += "." + id;
                    break;
                }
            }
            
            var errorInfo = error?.Code != null && error?.Message != null ? string.Format(" {0} - {1}", error.Code, error.Message) : "";
            return string.Format("{0} [{1}{2}]{3}", message, response.StatusCode, requestID, errorInfo);
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
}

