using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ZipDownloadRequestItemsField : ISerializable {
        /// <summary>
        /// The type of the item to add to the archive.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ZipDownloadRequestItemsTypeField>))]
        public StringEnum<ZipDownloadRequestItemsTypeField> Type { get; }

        /// <summary>
        /// The identifier of the item to add to the archive. When this item is
        /// a folder then this can not be the root folder with ID `0`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public ZipDownloadRequestItemsField(ZipDownloadRequestItemsTypeField type, string id) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal ZipDownloadRequestItemsField(StringEnum<ZipDownloadRequestItemsTypeField> type, string id) {
            Type = type;
            Id = id;
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}