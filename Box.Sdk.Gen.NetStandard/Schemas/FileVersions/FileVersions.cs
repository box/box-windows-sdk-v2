using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileVersions : ISerializable {
        /// <summary>
        /// One greater than the offset of the last entry in the entire collection.
        /// The total number of entries in the collection may be less than
        /// `total_count`.
        /// 
        /// This field is only returned for calls that use offset-based pagination.
        /// For marker-based paginated APIs, this field will be omitted.
        /// </summary>
        [JsonPropertyName("total_count")]
        public long? TotalCount { get; set; }

        /// <summary>
        /// The limit that was used for these entries. This will be the same as the
        /// `limit` query parameter unless that value exceeded the maximum value
        /// allowed. The maximum value varies by API.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; set; }

        /// <summary>
        /// The 0-based offset of the first entry in this set. This will be the same
        /// as the `offset` query parameter.
        /// 
        /// This field is only returned for calls that use offset-based pagination.
        /// For marker-based paginated APIs, this field will be omitted.
        /// </summary>
        [JsonPropertyName("offset")]
        public long? Offset { get; set; }

        /// <summary>
        /// The order by which items are returned.
        /// 
        /// This field is only returned for calls that use offset-based pagination.
        /// For marker-based paginated APIs, this field will be omitted.
        /// </summary>
        [JsonPropertyName("order")]
        public IReadOnlyList<FileVersionsOrderField> Order { get; set; }

        /// <summary>
        /// A list of file versions.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<FileVersionFull> Entries { get; set; }

        public FileVersions() {
            
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