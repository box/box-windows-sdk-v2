using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateCollaborationWhitelistExemptTargetRequestBody : ISerializable {
        /// <summary>
        /// The user to exempt.
        /// </summary>
        [JsonPropertyName("user")]
        public CreateCollaborationWhitelistExemptTargetRequestBodyUserField User { get; }

        public CreateCollaborationWhitelistExemptTargetRequestBody(CreateCollaborationWhitelistExemptTargetRequestBodyUserField user) {
            User = user;
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