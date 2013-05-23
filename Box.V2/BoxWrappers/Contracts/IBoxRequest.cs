using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Box.V2
{
    public interface IBoxRequest
    {
        Dictionary<string, string> HttpHeaders { get; }

        Dictionary<string, string> Parameters { get; }

        Dictionary<string, string> PayloadParameters { get; }

        string Payload { get; }

        string Path { get; }

        Uri Host { get; }

        RequestMethod Method { get; set;  }

        Uri AbsoluteUri { get; }

        Uri Uri { get; }

        string GetQueryString();
    }
}
