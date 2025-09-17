using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class ZipDownloadRequest : ISerializable {
        /// <summary>
        /// A list of items to add to the `zip` archive. These can
        /// be folders or files.
        /// </summary>
        [JsonPropertyName("items")]
        public IReadOnlyList<ZipDownloadRequestItemsField> Items { get; }

        /// <summary>
        /// The optional name of the `zip` archive. This name will be appended by the
        /// `.zip` file extension, for example `January Financials.zip`.
        /// </summary>
        [JsonPropertyName("download_file_name")]
        public string? DownloadFileName { get; init; }

        public ZipDownloadRequest(IReadOnlyList<ZipDownloadRequestItemsField> items) {
            Items = items;
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