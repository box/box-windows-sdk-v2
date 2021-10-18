using System;

namespace Box.V2
{
    /// <summary>
    /// A Box representation of a binay request.
    /// </summary>
    public class BoxBinaryRequest : BoxRequest
    {
        /// <summary>
        /// Instantiates a new Box binary request providing the Host URI.
        /// </summary>
        /// <param name="hostUri">The host URI.</param>
        public BoxBinaryRequest(Uri hostUri) : base(hostUri, string.Empty) { }

        /// <summary>
        /// The body content.
        /// </summary>
        public IBoxPart Part { get; set; }
    }
}
