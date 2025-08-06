using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentField : ISerializable {
        /// <summary>
        /// The ID reference of the
        /// requesting shield information barrier segment.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The type of the shield barrier segment for this member.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentTypeField>))]
        public StringEnum<CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentTypeField>? Type { get; init; }

        public CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentField() {
            
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