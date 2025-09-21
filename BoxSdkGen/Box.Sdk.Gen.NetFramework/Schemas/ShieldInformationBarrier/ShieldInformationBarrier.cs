using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrier : ISerializable {
        /// <summary>
        /// The unique identifier for the shield information barrier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of the shield information barrier.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldInformationBarrierTypeField>))]
        public StringEnum<ShieldInformationBarrierTypeField> Type { get; set; }

        /// <summary>
        /// The `type` and `id` of enterprise this barrier is under.
        /// </summary>
        [JsonPropertyName("enterprise")]
        public EnterpriseBase Enterprise { get; set; }

        /// <summary>
        /// Status of the shield information barrier.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<ShieldInformationBarrierStatusField>))]
        public StringEnum<ShieldInformationBarrierStatusField> Status { get; set; }

        /// <summary>
        /// ISO date time string when this
        /// shield information barrier object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The user who created this shield information barrier.
        /// </summary>
        [JsonPropertyName("created_by")]
        public UserBase CreatedBy { get; set; }

        /// <summary>
        /// ISO date time string when this shield information barrier was updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public System.DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// The user that updated this shield information barrier.
        /// </summary>
        [JsonPropertyName("updated_by")]
        public UserBase UpdatedBy { get; set; }

        /// <summary>
        /// ISO date time string when this shield information barrier was enabled.
        /// </summary>
        [JsonPropertyName("enabled_at")]
        public System.DateTimeOffset? EnabledAt { get; set; }

        [JsonPropertyName("enabled_by")]
        public UserBase EnabledBy { get; set; }

        public ShieldInformationBarrier() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}