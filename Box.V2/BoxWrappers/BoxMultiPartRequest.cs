using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public class BoxMultiPartRequest : BoxRequest
    {
        public BoxMultiPartRequest(Uri hostUri) : this(hostUri, string.Empty) { }

        public BoxMultiPartRequest(Uri hostUri, string path) 
            : base(hostUri, path) 
        {
            Parts = new List<IBoxFormPart>();
        }

        /// <summary>
        /// Request Method is always a POST in a multipart request
        /// </summary>
        public override RequestMethod Method
        {
            get
            {
                return RequestMethod.POST;
            }
        }

        public List<IBoxFormPart> Parts { get; set; }
    }
}
