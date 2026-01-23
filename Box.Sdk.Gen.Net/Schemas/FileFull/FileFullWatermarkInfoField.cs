using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileFullWatermarkInfoField : ISerializable {
        /// <summary>
        /// Specifies if this item has a watermark applied.
        /// </summary>
        [JsonPropertyName("is_watermarked")]
        public bool? IsWatermarked { get; init; }

        /// <summary>
        /// Specifies if the watermark is inherited from any parent folder in the hierarchy.
        /// </summary>
        [JsonPropertyName("is_watermark_inherited")]
        public bool? IsWatermarkInherited { get; init; }

        /// <summary>
        /// Specifies if the watermark is enforced by an access policy.
        /// </summary>
        [JsonPropertyName("is_watermarked_by_access_policy")]
        public bool? IsWatermarkedByAccessPolicy { get; init; }

        public FileFullWatermarkInfoField() {
            
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