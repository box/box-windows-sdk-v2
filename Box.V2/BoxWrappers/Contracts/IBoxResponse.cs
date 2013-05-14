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
        public T BoxModel { get; set; }

        /// <summary>
        /// Status of the response
        /// </summary>
        public ResponseStatus Status { get; set; }

        /// <summary>
        /// The error associated with an Error status
        /// This will be null in all other cases
        /// </summary>
        public BoxError Error { get; set; }
    }
}
