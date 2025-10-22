using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseFeatureSettingV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isstateSet")]
        protected bool _isStateSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscan_configureSet")]
        protected bool _isCanConfigureSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isis_configuredSet")]
        protected bool _isIsConfiguredSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isallowlistSet")]
        protected bool _isAllowlistSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdenylistSet")]
        protected bool _isDenylistSet { get; set; }

        protected string? _id { get; set; }

        protected string? _state { get; set; }

        protected bool? _canConfigure { get; set; }

        protected bool? _isConfigured { get; set; }

        protected IReadOnlyList<UserOrGroupReferenceV2025R0>? _allowlist { get; set; }

        protected IReadOnlyList<UserOrGroupReferenceV2025R0>? _denylist { get; set; }

        /// <summary>
        /// The identifier of the enterprise feature setting.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get => _id; init { _id = value; _isIdSet = true; } }

        /// <summary>
        /// The feature.
        /// </summary>
        [JsonPropertyName("feature")]
        public EnterpriseFeatureSettingV2025R0FeatureField? Feature { get; init; }

        /// <summary>
        /// The state of the feature.
        /// </summary>
        [JsonPropertyName("state")]
        public string? State { get => _state; init { _state = value; _isStateSet = true; } }

        /// <summary>
        /// Whether the feature can be configured.
        /// </summary>
        [JsonPropertyName("can_configure")]
        public bool? CanConfigure { get => _canConfigure; init { _canConfigure = value; _isCanConfigureSet = true; } }

        /// <summary>
        /// Whether the feature is configured.
        /// </summary>
        [JsonPropertyName("is_configured")]
        public bool? IsConfigured { get => _isConfigured; init { _isConfigured = value; _isIsConfiguredSet = true; } }

        /// <summary>
        /// Enterprise feature setting is enabled for only this set of users and groups.
        /// </summary>
        [JsonPropertyName("allowlist")]
        public IReadOnlyList<UserOrGroupReferenceV2025R0>? Allowlist { get => _allowlist; init { _allowlist = value; _isAllowlistSet = true; } }

        /// <summary>
        /// Enterprise feature setting is enabled for everyone except this set of users and groups.
        /// </summary>
        [JsonPropertyName("denylist")]
        public IReadOnlyList<UserOrGroupReferenceV2025R0>? Denylist { get => _denylist; init { _denylist = value; _isDenylistSet = true; } }

        public EnterpriseFeatureSettingV2025R0() {
            
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