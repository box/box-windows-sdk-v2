using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ZipDownloadNameConflictsField : ISerializable {
        /// <summary>
        /// The identifier of the item.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of this item.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ZipDownloadNameConflictsTypeField>))]
        public StringEnum<ZipDownloadNameConflictsTypeField> Type { get; set; }

        /// <summary>
        /// Box Developer Documentation.
        /// </summary>
        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        /// <summary>
        /// The new name of this item as it will appear in the
        /// downloaded `zip` archive.
        /// </summary>
        [JsonPropertyName("download_name")]
        public string DownloadName { get; set; }

        public ZipDownloadNameConflictsField() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}