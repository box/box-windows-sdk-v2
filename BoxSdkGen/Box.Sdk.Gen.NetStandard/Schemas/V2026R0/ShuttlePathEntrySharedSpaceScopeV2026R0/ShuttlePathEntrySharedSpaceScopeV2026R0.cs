using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathEntrySharedSpaceScopeV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isspaceSet")]
        protected bool _isSpaceSet { get; set; }

        protected string _space { get; set; }

        /// <summary>
        /// The value will always be `shared_space`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathEntrySharedSpaceScopeV2026R0TypeField>))]
        public StringEnum<ShuttlePathEntrySharedSpaceScopeV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The display name of the shared container this entry belongs to.
        /// `null` when the system has a single implicit container. Maps to
        /// `space` on a shared-space path mapping location.
        /// </summary>
        [JsonPropertyName("space")]
        public string Space { get => _space; set { _space = value; _isSpaceSet = true; } }

        public ShuttlePathEntrySharedSpaceScopeV2026R0(ShuttlePathEntrySharedSpaceScopeV2026R0TypeField type = ShuttlePathEntrySharedSpaceScopeV2026R0TypeField.SharedSpace) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathEntrySharedSpaceScopeV2026R0(StringEnum<ShuttlePathEntrySharedSpaceScopeV2026R0TypeField> type) {
            Type = ShuttlePathEntrySharedSpaceScopeV2026R0TypeField.SharedSpace;
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