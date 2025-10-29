using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class KeysafeSettingsV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscloud_providerSet")]
        protected bool _isCloudProviderSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iskey_idSet")]
        protected bool _isKeyIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isaccount_idSet")]
        protected bool _isAccountIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_islocation_idSet")]
        protected bool _isLocationIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isproject_idSet")]
        protected bool _isProjectIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iskeyring_idSet")]
        protected bool _isKeyringIdSet { get; set; }

        protected string? _cloudProvider { get; set; }

        protected string? _keyId { get; set; }

        protected string? _accountId { get; set; }

        protected string? _locationId { get; set; }

        protected string? _projectId { get; set; }

        protected string? _keyringId { get; set; }

        /// <summary>
        /// Whether KeySafe addon is enabled for the enterprise.
        /// </summary>
        [JsonPropertyName("keysafe_enabled")]
        public bool? KeysafeEnabled { get; init; }

        /// <summary>
        /// The cloud provider.
        /// </summary>
        [JsonPropertyName("cloud_provider")]
        public string? CloudProvider { get => _cloudProvider; init { _cloudProvider = value; _isCloudProviderSet = true; } }

        /// <summary>
        /// The key ID.
        /// </summary>
        [JsonPropertyName("key_id")]
        public string? KeyId { get => _keyId; init { _keyId = value; _isKeyIdSet = true; } }

        /// <summary>
        /// The account ID.
        /// </summary>
        [JsonPropertyName("account_id")]
        public string? AccountId { get => _accountId; init { _accountId = value; _isAccountIdSet = true; } }

        /// <summary>
        /// The location ID.
        /// </summary>
        [JsonPropertyName("location_id")]
        public string? LocationId { get => _locationId; init { _locationId = value; _isLocationIdSet = true; } }

        /// <summary>
        /// The project ID.
        /// </summary>
        [JsonPropertyName("project_id")]
        public string? ProjectId { get => _projectId; init { _projectId = value; _isProjectIdSet = true; } }

        /// <summary>
        /// The key ring ID.
        /// </summary>
        [JsonPropertyName("keyring_id")]
        public string? KeyringId { get => _keyringId; init { _keyringId = value; _isKeyringIdSet = true; } }

        public KeysafeSettingsV2025R0() {
            
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