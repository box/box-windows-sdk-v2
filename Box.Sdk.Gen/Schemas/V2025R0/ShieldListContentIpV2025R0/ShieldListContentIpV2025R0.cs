using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldListContentIpV2025R0 : ISerializable {
        /// <summary>
        /// The type of content in the shield list.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldListContentIpV2025R0TypeField>))]
        public StringEnum<ShieldListContentIpV2025R0TypeField> Type { get; }

        /// <summary>
        /// List of ips and cidrs.
        /// </summary>
        [JsonPropertyName("ip_addresses")]
        public IReadOnlyList<string> IpAddresses { get; }

        public ShieldListContentIpV2025R0(IReadOnlyList<string> ipAddresses, ShieldListContentIpV2025R0TypeField type = ShieldListContentIpV2025R0TypeField.Ip) {
            Type = type;
            IpAddresses = ipAddresses;
        }
        
        [JsonConstructorAttribute]
        internal ShieldListContentIpV2025R0(IReadOnlyList<string> ipAddresses, StringEnum<ShieldListContentIpV2025R0TypeField> type) {
            Type = ShieldListContentIpV2025R0TypeField.Ip;
            IpAddresses = ipAddresses;
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