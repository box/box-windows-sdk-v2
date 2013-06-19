using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Box.V2
{
    public interface IBoxRequest
    {
        IList<KeyValuePair<string, string>> HttpHeaders { get; }

        Dictionary<string, string> Parameters { get; }

        string Path { get; }

        Uri Host { get; }

        RequestMethod Method { get; set;  }

        Uri AbsoluteUri { get; }

        Uri Uri { get; }

        string GetQueryString();
    }
}
