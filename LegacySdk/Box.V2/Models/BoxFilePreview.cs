using System.IO;
using System.Net;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents a file preview response.
    /// </summary>
    public class BoxFilePreview
    {
        /// <summary>
        /// The stream of the preview.
        /// </summary>
        public virtual Stream PreviewStream { get; set; }

        /// <summary>
        /// Returned HTTP status code from the preview request. Refer to API page for possible return values.
        /// </summary>
        public virtual HttpStatusCode ReturnedStatusCode { get; set; }

        /// <summary>
        /// Total pages in the file to preview
        /// </summary>
        public virtual int TotalPages { get; set; }

        /// <summary>
        /// The page number of this preview out of the total availabe pages in the file
        /// </summary>
        public virtual int CurrentPage { get; set; }
    }
}
