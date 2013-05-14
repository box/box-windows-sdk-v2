using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Services
{
    public class JsonResponseParser : IResponseParser
    {
        public T Parse<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
