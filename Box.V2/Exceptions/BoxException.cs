using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
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

