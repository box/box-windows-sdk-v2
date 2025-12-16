using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class PatchMetadataTaxonomiesIdIdLevelsIdRequestBody : ISerializable {
        /// <summary>
        /// The display name of the taxonomy level.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; }

        /// <summary>
        /// The description of the taxonomy level.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        public PatchMetadataTaxonomiesIdIdLevelsIdRequestBody(string displayName) {
            DisplayName = displayName;
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