using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Files that will be signed, which are copies of the original source files.
    /// A new version of these files are created as signers sign and can be downloaded at any point in the signing process.
    /// </summary>
    public class BoxSignRequestSignFiles
    {
        public const string FieldFiles = "files";
        public const string FieldIsReadyForDownload = "is_ready_for_download";

        /// <summary>
        /// Files that will be signed, which are copies of the original source files.
        /// </summary>
        [JsonProperty(PropertyName = FieldFiles)]
        public virtual List<BoxFile> Files { get; private set; }

        /// <summary>
        /// Indicates whether the sign_files documents are processing and the PDFs may be out of date. A change to any document requires processing on all sign_files.
        /// We recommended waiting until processing is finished (and this value is true) before downloading the PDFs.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsReadyForDownload)]
        public virtual bool IsReadyForDownload { get; private set; }
    }
}
