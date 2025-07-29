using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateShieldInformationBarrierSegmentMemberRequestBody : ISerializable {
        /// <summary>
        /// A type of the shield barrier segment member.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateShieldInformationBarrierSegmentMemberRequestBodyTypeField>))]
        public StringEnum<CreateShieldInformationBarrierSegmentMemberRequestBodyTypeField>? Type { get; init; }

        [JsonPropertyName("shield_information_barrier")]
        public ShieldInformationBarrierBase? ShieldInformationBarrier { get; init; }

        /// <summary>
        /// The `type` and `id` of the
        /// requested shield information barrier segment.
        /// </summary>
        [JsonPropertyName("shield_information_barrier_segment")]
        public CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentField ShieldInformationBarrierSegment { get; }

        /// <summary>
        /// User to which restriction will be applied.
        /// </summary>
        [JsonPropertyName("user")]
        public UserBase User { get; }

        public CreateShieldInformationBarrierSegmentMemberRequestBody(CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentField shieldInformationBarrierSegment, UserBase user) {
            ShieldInformationBarrierSegment = shieldInformationBarrierSegment;
            User = user;
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