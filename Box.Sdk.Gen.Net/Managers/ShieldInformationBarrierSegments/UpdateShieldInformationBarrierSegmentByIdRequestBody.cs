using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateShieldInformationBarrierSegmentByIdRequestBody : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdescriptionSet")]
        protected bool _isDescriptionSet { get; set; }

        protected string? _description { get; set; }

        /// <summary>
        /// The updated name for the shield information barrier segment.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The updated description for
        /// the shield information barrier segment.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get => _description; init { _description = value; _isDescriptionSet = true; } }

        public UpdateShieldInformationBarrierSegmentByIdRequestBody() {
            
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