using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TrashFolderPathCollectionField : ISerializable {
        /// <summary>
        /// The number of folders in this list.
        /// </summary>
        [JsonPropertyName("total_count")]
        public long TotalCount { get; }

        /// <summary>
        /// Array of folders for this item's path collection.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<TrashFolderPathCollectionEntriesField> Entries { get; }

        public TrashFolderPathCollectionField(long totalCount, IReadOnlyList<TrashFolderPathCollectionEntriesField> entries) {
            TotalCount = totalCount;
            Entries = entries;
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