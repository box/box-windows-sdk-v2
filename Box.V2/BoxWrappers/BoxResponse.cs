using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public class BoxResponse<T>
    {
        public T BoxModel { get; set;  }

        public ResponseStatus Status { get; set; }


    }

    public enum ResponseStatus
    {
        Unknown,
        Success,
        Error
    }
}
