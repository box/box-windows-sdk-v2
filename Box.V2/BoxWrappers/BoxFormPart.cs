using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public class BoxStringFormPart : IBoxFormPart<string>
    {
        public string Name { get; set; } 

        public string Value { get; set; }
    }

    public class BoxFileFormPart : IBoxFormPart<Stream>
    {
        public string Name { get; set; }

        public Stream Value { get; set; }

        public string FileName { get; set; }
    }
}
