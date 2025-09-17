using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class IntegrationMappingSlackOptions : ISerializable {
        /// <summary>
        /// Indicates whether or not channel member
        /// access to the underlying box item
        /// should be automatically managed.
        /// Depending on type of channel, access is managed
        /// through creating collaborations or shared links.
        /// </summary>
        [JsonPropertyName("is_access_management_disabled")]
        public bool? IsAccessManagementDisabled { get; init; }

        public IntegrationMappingSlackOptions() {
            
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