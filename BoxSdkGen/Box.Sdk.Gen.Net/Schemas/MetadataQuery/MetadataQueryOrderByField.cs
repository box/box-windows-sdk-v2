using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataQueryOrderByField : ISerializable {
        /// <summary>
        /// The metadata template field to order by.
        /// 
        /// The `field_key` represents the `key` value of a field from the
        /// metadata template being searched for.
        /// </summary>
        [JsonPropertyName("field_key")]
        public string? FieldKey { get; init; }

        /// <summary>
        /// The direction to order by, either ascending or descending.
        /// 
        /// The `ordering` direction must be the same for each item in the
        /// array.
        /// </summary>
        [JsonPropertyName("direction")]
        [JsonConverter(typeof(StringEnumConverter<MetadataQueryOrderByDirectionField>))]
        public StringEnum<MetadataQueryOrderByDirectionField>? Direction { get; init; }

        public MetadataQueryOrderByField() {
            
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