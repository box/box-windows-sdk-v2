using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrierReport : ShieldInformationBarrierReportBase, ISerializable {
        [JsonPropertyName("shield_information_barrier")]
        public ShieldInformationBarrierReference ShieldInformationBarrier { get; set; }

        /// <summary>
        /// Status of the shield information report.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<ShieldInformationBarrierReportStatusField>))]
        public StringEnum<ShieldInformationBarrierReportStatusField> Status { get; set; }

        [JsonPropertyName("details")]
        public ShieldInformationBarrierReportDetails Details { get; set; }

        /// <summary>
        /// ISO date time string when this
        /// shield information barrier report object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserBase CreatedBy { get; set; }

        /// <summary>
        /// ISO date time string when this
        /// shield information barrier report was updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset? UpdatedAt { get; set; }

        public ShieldInformationBarrierReport() {
            
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