using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class TerminateUsersSessionsRequestBody : ISerializable {
        /// <summary>
        /// A list of user IDs.
        /// </summary>
        [JsonPropertyName("user_ids")]
        public IReadOnlyList<string> UserIds { get; }

        /// <summary>
        /// A list of user logins.
        /// </summary>
        [JsonPropertyName("user_logins")]
        public IReadOnlyList<string> UserLogins { get; }

        public TerminateUsersSessionsRequestBody(IReadOnlyList<string> userIds, IReadOnlyList<string> userLogins) {
            UserIds = userIds;
            UserLogins = userLogins;
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