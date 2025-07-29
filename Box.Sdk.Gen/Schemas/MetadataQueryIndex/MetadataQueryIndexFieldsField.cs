using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataQueryIndexFieldsField : ISerializable {
        /// <summary>
        /// The metadata template field key.
        /// </summary>
        [JsonPropertyName("key")]
        public string? Key { get; init; }

        /// <summary>
        /// The sort direction of the field.
        /// </summary>
        [JsonPropertyName("sort_direction")]
        [JsonConverter(typeof(StringEnumConverter<MetadataQueryIndexFieldsSortDirectionField>))]
        public StringEnum<MetadataQueryIndexFieldsSortDirectionField>? SortDirection { get; init; }

        public MetadataQueryIndexFieldsField() {
            
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