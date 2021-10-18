using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for creating a new file upload session
    /// </summary>
    public class BoxFileUploadSessionRequest
    {
        /// <summary>
        /// The parent folder_id is the folder where the upload should happen.
        /// </summary>
        [JsonProperty(PropertyName = "folder_id")]
        public string FolderId { get; set; }

        /// <summary>
        /// The total number of bytes in the file to be uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "file_size")]
        public long FileSize { get; set; }

        /// <summary>
        /// Name of new file.
        /// </summary>
        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }
    }
}
