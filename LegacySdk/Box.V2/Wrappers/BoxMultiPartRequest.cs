using System;
using System.Collections.Generic;

namespace Box.V2
{
    /// <summary>
    /// A Box representation of a multi-part form request
    /// </summary>
    public class BoxMultiPartRequest : BoxRequest
    {
        /// <summary>
        /// Instantiates a new Box multi-part form request providing the Host URI
        /// </summary>
        /// <param name="hostUri">The host URI for the multi-part request</param>
        public BoxMultiPartRequest(Uri hostUri) : this(hostUri, string.Empty) { }

        /// <summary>
        /// Instantiates a new Box multi-part form request providing the Host URI
        /// </summary>
        /// <param name="hostUri">The host URI for the multi-part request</param>
        /// <param name="path">The path for the multi-part request</param>
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
                return RequestMethod.Post;
            }
        }

        /// <summary>
        /// The different parts of the multi-part form
        /// </summary>
        public List<IBoxFormPart> Parts { get; set; }
    }
}
