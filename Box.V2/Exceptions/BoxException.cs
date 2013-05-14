using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Exceptions
{
    public class BoxException : Exception
    {
        public BoxException() : base() { }

        public BoxException(string message) : base(message) { }

        public BoxException(string message, Exception innerException) : base(message, innerException) { }
    }
}
