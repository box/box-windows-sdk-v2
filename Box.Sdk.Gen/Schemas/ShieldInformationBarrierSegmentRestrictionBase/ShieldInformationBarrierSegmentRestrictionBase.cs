using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrierSegmentRestrictionBase : ISerializable {
        /// <summary>
        /// Shield information barrier segment restriction.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldInformationBarrierSegmentRestrictionBaseTypeField>))]
        public StringEnum<ShieldInformationBarrierSegmentRestrictionBaseTypeField>? Type { get; init; }

        /// <summary>
        /// The unique identifier for the
        /// shield information barrier segment restriction.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public ShieldInformationBarrierSegmentRestrictionBase() {
            
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