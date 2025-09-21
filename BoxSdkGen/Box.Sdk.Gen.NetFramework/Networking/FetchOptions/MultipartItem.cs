using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public class MultipartItem {
        /// <summary>
        /// Name of the part
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// Data of the part
        /// </summary>
        public SerializedData Data { get; set; }

        /// <summary>
        /// File stream of the part
        /// </summary>
        public System.IO.Stream FileStream { get; set; }

        /// <summary>
        /// File name of the part
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Content type of the part
        /// </summary>
        public string ContentType { get; set; }

        public MultipartItem(string partName) {
            PartName = partName;
        }
    }
}