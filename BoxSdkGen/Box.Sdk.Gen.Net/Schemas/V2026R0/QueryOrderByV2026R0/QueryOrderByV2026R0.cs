using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class QueryOrderByV2026R0 : ISerializable {
        /// <summary>
        /// The fully qualified field key to sort by.
        /// </summary>
        [JsonPropertyName("field_key")]
        public string FieldKey { get; }

        /// <summary>
        /// The direction in which results are ordered.
        /// </summary>
        [JsonPropertyName("direction")]
        [JsonConverter(typeof(StringEnumConverter<QueryOrderByV2026R0DirectionField>))]
        public StringEnum<QueryOrderByV2026R0DirectionField> Direction { get; }

        public QueryOrderByV2026R0(string fieldKey, QueryOrderByV2026R0DirectionField direction) {
            FieldKey = fieldKey;
            Direction = direction;
        }
        
        [JsonConstructorAttribute]
        internal QueryOrderByV2026R0(string fieldKey, StringEnum<QueryOrderByV2026R0DirectionField> direction) {
            FieldKey = fieldKey;
            Direction = direction;
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