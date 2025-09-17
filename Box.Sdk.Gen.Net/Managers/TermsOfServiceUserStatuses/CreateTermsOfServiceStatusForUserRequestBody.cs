using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateTermsOfServiceStatusForUserRequestBody : ISerializable {
        /// <summary>
        /// The terms of service to set the status for.
        /// </summary>
        [JsonPropertyName("tos")]
        public CreateTermsOfServiceStatusForUserRequestBodyTosField Tos { get; }

        /// <summary>
        /// The user to set the status for.
        /// </summary>
        [JsonPropertyName("user")]
        public CreateTermsOfServiceStatusForUserRequestBodyUserField User { get; }

        /// <summary>
        /// Whether the user has accepted the terms.
        /// </summary>
        [JsonPropertyName("is_accepted")]
        public bool IsAccepted { get; }

        public CreateTermsOfServiceStatusForUserRequestBody(CreateTermsOfServiceStatusForUserRequestBodyTosField tos, CreateTermsOfServiceStatusForUserRequestBodyUserField user, bool isAccepted) {
            Tos = tos;
            User = user;
            IsAccepted = isAccepted;
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