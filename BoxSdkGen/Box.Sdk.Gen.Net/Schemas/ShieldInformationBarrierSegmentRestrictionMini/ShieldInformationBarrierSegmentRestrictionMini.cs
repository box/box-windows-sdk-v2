using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrierSegmentRestrictionMini : ShieldInformationBarrierSegmentRestrictionBase, ISerializable {
        /// <summary>
        /// The `type` and `id` of the
        /// requested shield information barrier segment.
        /// </summary>
        [JsonPropertyName("shield_information_barrier_segment")]
        public ShieldInformationBarrierSegmentRestrictionMiniShieldInformationBarrierSegmentField ShieldInformationBarrierSegment { get; }

        /// <summary>
        /// The `type` and `id` of the
        /// restricted shield information barrier segment.
        /// </summary>
        [JsonPropertyName("restricted_segment")]
        public ShieldInformationBarrierSegmentRestrictionMiniRestrictedSegmentField RestrictedSegment { get; }

        public ShieldInformationBarrierSegmentRestrictionMini(ShieldInformationBarrierSegmentRestrictionMiniShieldInformationBarrierSegmentField shieldInformationBarrierSegment, ShieldInformationBarrierSegmentRestrictionMiniRestrictedSegmentField restrictedSegment) {
            ShieldInformationBarrierSegment = shieldInformationBarrierSegment;
            RestrictedSegment = restrictedSegment;
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}