using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public class MultipartItem {
        /// <summary>
        /// Name of the part
        /// </summary>
        public string PartName { get; }

        /// <summary>
        /// Data of the part
        /// </summary>
        public SerializedData? Data { get; init; }

        /// <summary>
        /// File stream of the part
        /// </summary>
        public System.IO.Stream? FileStream { get; init; }

        /// <summary>
        /// File name of the part
        /// </summary>
        public string? FileName { get; init; }

        /// <summary>
        /// Content type of the part
        /// </summary>
        public string? ContentType { get; init; }

        public MultipartItem(string partName) {
            PartName = partName;
        }
    }
}