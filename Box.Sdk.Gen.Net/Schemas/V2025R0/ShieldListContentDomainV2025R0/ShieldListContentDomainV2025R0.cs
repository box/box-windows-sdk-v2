using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldListContentDomainV2025R0 : ISerializable {
        /// <summary>
        /// The type of content in the shield list.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldListContentDomainV2025R0TypeField>))]
        public StringEnum<ShieldListContentDomainV2025R0TypeField> Type { get; }

        /// <summary>
        /// List of domain.
        /// </summary>
        [JsonPropertyName("domains")]
        public IReadOnlyList<string> Domains { get; }

        public ShieldListContentDomainV2025R0(IReadOnlyList<string> domains, ShieldListContentDomainV2025R0TypeField type = ShieldListContentDomainV2025R0TypeField.Domain) {
            Type = type;
            Domains = domains;
        }
        
        [JsonConstructorAttribute]
        internal ShieldListContentDomainV2025R0(IReadOnlyList<string> domains, StringEnum<ShieldListContentDomainV2025R0TypeField> type) {
            Type = ShieldListContentDomainV2025R0TypeField.Domain;
            Domains = domains;
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