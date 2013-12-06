using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// Http Status code for the response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}

