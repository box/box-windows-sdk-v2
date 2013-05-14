using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Services
{
    public interface IResponseParser
    {
        T Parse<T>(string response);
    }
}
