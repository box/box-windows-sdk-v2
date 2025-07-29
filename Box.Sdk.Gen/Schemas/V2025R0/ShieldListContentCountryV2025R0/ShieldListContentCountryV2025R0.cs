using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldListContentCountryV2025R0 : ISerializable {
        /// <summary>
        /// The type of content in the shield list.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldListContentCountryV2025R0TypeField>))]
        public StringEnum<ShieldListContentCountryV2025R0TypeField> Type { get; }

        /// <summary>
        /// List of country codes values.
        /// </summary>
        [JsonPropertyName("country_codes")]
        public IReadOnlyList<string> CountryCodes { get; }

        public ShieldListContentCountryV2025R0(IReadOnlyList<string> countryCodes, ShieldListContentCountryV2025R0TypeField type = ShieldListContentCountryV2025R0TypeField.Country) {
            Type = type;
            CountryCodes = countryCodes;
        }
        
        [JsonConstructorAttribute]
        internal ShieldListContentCountryV2025R0(IReadOnlyList<string> countryCodes, StringEnum<ShieldListContentCountryV2025R0TypeField> type) {
            Type = ShieldListContentCountryV2025R0TypeField.Country;
            CountryCodes = countryCodes;
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