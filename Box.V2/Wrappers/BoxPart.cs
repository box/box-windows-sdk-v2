using System.IO;

namespace Box.V2
{
    /// <summary>
    /// A Box representation of the file part.
    /// </summary>
    public class BoxFilePart : IBoxPart<Stream>
    {
        public Stream Value { get; set; }
    }
}
