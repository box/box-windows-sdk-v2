using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public interface IBoxResponse<T>
    {
        /// <summary>
        /// The parsed model from a successful response
        /// </summary>
        T BoxModel { get; set; }

        /// <summary>
        /// The full response string from the request
        /// </summary>
        string ContentString { get; set; }

        /// <summary>
        /// Status of the response
        /// </summary>
        ResponseStatus Status { get; set; }

        /// <summary>
        /// The error associated with an Error status
        /// This will be null in all other cases
        /// </summary>
        BoxError Error { get; set; }
    }
}
