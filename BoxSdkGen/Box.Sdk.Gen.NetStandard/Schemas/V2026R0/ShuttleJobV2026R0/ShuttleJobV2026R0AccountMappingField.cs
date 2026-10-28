using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobV2026R0AccountMappingField : ISerializable {
        /// <summary>
        /// Whether account mapping is enabled for the shuttle job.
        /// </summary>
        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// The strategy used to resolve permission conflicts. `null` when
        /// unset.
        /// </summary>
        [JsonPropertyName("permission_conflict_strategy")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobPermissionConflictStrategyV2026R0>))]
        public StringEnum<ShuttleJobPermissionConflictStrategyV2026R0> PermissionConflictStrategy { get; set; }

        /// <summary>
        /// Whether external shares are mapped during the migration.
        /// </summary>
        [JsonPropertyName("are_external_shares_mapped")]
        public bool? AreExternalSharesMapped { get; set; }

        /// <summary>
        /// Whether permissions are mapped during the migration.
        /// </summary>
        [JsonPropertyName("are_permissions_mapped")]
        public bool? ArePermissionsMapped { get; set; }

        public ShuttleJobV2026R0AccountMappingField(bool isEnabled) {
            IsEnabled = isEnabled;
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