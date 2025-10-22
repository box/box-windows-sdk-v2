using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationSecurityV2025R0EnforcedMfaFrequencyFieldValueField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdaysSet")]
        protected bool _isDaysSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ishoursSet")]
        protected bool _isHoursSet { get; set; }

        protected long? _days { get; set; }

        protected long? _hours { get; set; }

        /// <summary>
        /// Number of days before the user is required to authenticate again.
        /// </summary>
        [JsonPropertyName("days")]
        public long? Days { get => _days; init { _days = value; _isDaysSet = true; } }

        /// <summary>
        /// Number of hours before the user is required to authenticate again.
        /// </summary>
        [JsonPropertyName("hours")]
        public long? Hours { get => _hours; init { _hours = value; _isHoursSet = true; } }

        public EnterpriseConfigurationSecurityV2025R0EnforcedMfaFrequencyFieldValueField() {
            
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