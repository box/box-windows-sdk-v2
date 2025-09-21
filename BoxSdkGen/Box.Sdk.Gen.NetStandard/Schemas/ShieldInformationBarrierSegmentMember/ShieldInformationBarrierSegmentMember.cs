using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrierSegmentMember : ShieldInformationBarrierSegmentMemberMini, ISerializable {
        [JsonPropertyName("shield_information_barrier")]
        public ShieldInformationBarrierBase ShieldInformationBarrier { get; set; }

        /// <summary>
        /// The `type` and `id` of the requested
        /// shield information barrier segment.
        /// </summary>
        [JsonPropertyName("shield_information_barrier_segment")]
        public ShieldInformationBarrierSegmentMemberShieldInformationBarrierSegmentField ShieldInformationBarrierSegment { get; set; }

        /// <summary>
        /// ISO date time string when this shield
        /// information barrier object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserBase CreatedBy { get; set; }

        /// <summary>
        /// ISO date time string when this
        /// shield information barrier segment Member was updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("updated_by")]
        public UserBase UpdatedBy { get; set; }

        public ShieldInformationBarrierSegmentMember() {
            
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