using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class CollaborationAcceptanceRequirementsStatusTermsOfServiceRequirementField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isis_acceptedSet")]
        protected bool _isIsAcceptedSet { get; set; }

        protected bool? _isAccepted { get; set; }

        /// <summary>
        /// Whether or not the terms of service have been accepted.  The
        /// field is `null` when there is no terms of service required.
        /// </summary>
        [JsonPropertyName("is_accepted")]
        public bool? IsAccepted { get => _isAccepted; init { _isAccepted = value; _isIsAcceptedSet = true; } }

        [JsonPropertyName("terms_of_service")]
        public TermsOfServiceBase? TermsOfService { get; init; }

        public CollaborationAcceptanceRequirementsStatusTermsOfServiceRequirementField() {
            
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