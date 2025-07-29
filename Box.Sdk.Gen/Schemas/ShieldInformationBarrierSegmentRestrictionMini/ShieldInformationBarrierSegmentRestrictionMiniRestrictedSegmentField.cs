using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrierSegmentRestrictionMiniRestrictedSegmentField : ISerializable {
        /// <summary>
        /// The ID reference of the
        /// restricted shield information barrier segment.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The type of the shield information segment.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldInformationBarrierSegmentRestrictionMiniRestrictedSegmentTypeField>))]
        public StringEnum<ShieldInformationBarrierSegmentRestrictionMiniRestrictedSegmentTypeField>? Type { get; init; }

        public ShieldInformationBarrierSegmentRestrictionMiniRestrictedSegmentField() {
            
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