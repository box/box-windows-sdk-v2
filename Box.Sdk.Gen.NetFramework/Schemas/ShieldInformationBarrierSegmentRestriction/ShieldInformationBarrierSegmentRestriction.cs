using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrierSegmentRestriction : ShieldInformationBarrierSegmentRestrictionMini, ISerializable {
        [JsonPropertyName("shield_information_barrier")]
        public ShieldInformationBarrierBase ShieldInformationBarrier { get; set; }

        /// <summary>
        /// ISO date time string when this
        /// shield information barrier
        /// Segment Restriction object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserBase CreatedBy { get; set; }

        /// <summary>
        /// ISO date time string when this
        /// shield information barrier segment
        /// Restriction was updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("updated_by")]
        public UserBase UpdatedBy { get; set; }

        public ShieldInformationBarrierSegmentRestriction(ShieldInformationBarrierSegmentRestrictionMiniShieldInformationBarrierSegmentField shieldInformationBarrierSegment, ShieldInformationBarrierSegmentRestrictionMiniRestrictedSegmentField restrictedSegment) : base(shieldInformationBarrierSegment, restrictedSegment) {
            
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}