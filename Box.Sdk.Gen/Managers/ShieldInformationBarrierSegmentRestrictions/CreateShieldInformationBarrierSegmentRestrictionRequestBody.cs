using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateShieldInformationBarrierSegmentRestrictionRequestBody : ISerializable {
        /// <summary>
        /// The type of the shield barrier segment
        /// restriction for this member.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField>))]
        public StringEnum<CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField> Type { get; }

        [JsonPropertyName("shield_information_barrier")]
        public ShieldInformationBarrierBase? ShieldInformationBarrier { get; init; }

        /// <summary>
        /// The `type` and `id` of the requested
        /// shield information barrier segment.
        /// </summary>
        [JsonPropertyName("shield_information_barrier_segment")]
        public CreateShieldInformationBarrierSegmentRestrictionRequestBodyShieldInformationBarrierSegmentField ShieldInformationBarrierSegment { get; }

        /// <summary>
        /// The `type` and `id` of the restricted
        /// shield information barrier segment.
        /// </summary>
        [JsonPropertyName("restricted_segment")]
        public CreateShieldInformationBarrierSegmentRestrictionRequestBodyRestrictedSegmentField RestrictedSegment { get; }

        public CreateShieldInformationBarrierSegmentRestrictionRequestBody(CreateShieldInformationBarrierSegmentRestrictionRequestBodyShieldInformationBarrierSegmentField shieldInformationBarrierSegment, CreateShieldInformationBarrierSegmentRestrictionRequestBodyRestrictedSegmentField restrictedSegment, CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField type = CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField.ShieldInformationBarrierSegmentRestriction) {
            Type = type;
            ShieldInformationBarrierSegment = shieldInformationBarrierSegment;
            RestrictedSegment = restrictedSegment;
        }
        
        [JsonConstructorAttribute]
        internal CreateShieldInformationBarrierSegmentRestrictionRequestBody(CreateShieldInformationBarrierSegmentRestrictionRequestBodyShieldInformationBarrierSegmentField shieldInformationBarrierSegment, CreateShieldInformationBarrierSegmentRestrictionRequestBodyRestrictedSegmentField restrictedSegment, StringEnum<CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField> type) {
            Type = CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField.ShieldInformationBarrierSegmentRestriction;
            ShieldInformationBarrierSegment = shieldInformationBarrierSegment;
            RestrictedSegment = restrictedSegment;
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