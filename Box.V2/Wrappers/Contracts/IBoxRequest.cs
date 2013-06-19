using System;
using System.Collections.Generic;

namespace Box.V2
{
    public interface IBoxRequest
    {
        Uri Host { get; }

        string Path { get; }

        Dictionary<string, string> HttpHeaders { get; }

        Dictionary<string, string> Parameters { get; }

        Dictionary<string, string> PayloadParameters { get; }

        string Payload { get; set;  }

        string Authorization { get; set; }

        RequestMethod Method { get; set;  }

        Uri AbsoluteUri { get; }

        Uri Uri { get; }

        string GetQueryString();
    }
}
