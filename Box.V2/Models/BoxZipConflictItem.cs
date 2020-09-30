using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a naming conflict creating a zip file for an item
    /// </summary>
    public class BoxZipConflictItem
    {
        public const string FieldId = "id";
        public const string FieldType = "type";
        public const string FieldOriginalName = "original_name";
        public const string FieldDownloadName = "download_name";

        /// <summary>
        /// The Id of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldId)]
        public string Id { get; private set; }

        /// <summary>
        /// The type of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; private set; }

        /// <summary>
        /// The original name of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldOriginalName)]
        public string OriginalName { get; private set; }

        /// <summary>
        /// the new name of the item when it downloads that resolves the conflict
        /// </summary>
        [JsonProperty(PropertyName = FieldDownloadName)]
        public string DownloadName { get; private set; }
    }
}
