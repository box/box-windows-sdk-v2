using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateInviteRequestBody : ISerializable {
        /// <summary>
        /// The enterprise to invite the user to.
        /// </summary>
        [JsonPropertyName("enterprise")]
        public CreateInviteRequestBodyEnterpriseField Enterprise { get; }

        /// <summary>
        /// The user to invite.
        /// </summary>
        [JsonPropertyName("actionable_by")]
        public CreateInviteRequestBodyActionableByField ActionableBy { get; }

        public CreateInviteRequestBody(CreateInviteRequestBodyEnterpriseField enterprise, CreateInviteRequestBodyActionableByField actionableBy) {
            Enterprise = enterprise;
            ActionableBy = actionableBy;
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