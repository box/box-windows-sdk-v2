using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateUserEmailAliasRequestBody : ISerializable {
        /// <summary>
        /// The email address to add to the account as an alias.
        /// 
        /// Note: The domain of the email alias needs to be registered
        ///  to your enterprise.
        /// See the [domain verification guide](
        ///   https://support.box.com/hc/en-us/articles/4408619650579-Domain-Verification
        ///   ) for steps to add a new domain.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; }

        public CreateUserEmailAliasRequestBody(string email) {
            Email = email;
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