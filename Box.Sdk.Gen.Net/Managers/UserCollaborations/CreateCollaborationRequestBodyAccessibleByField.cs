using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateCollaborationRequestBodyAccessibleByField : ISerializable {
        /// <summary>
        /// The type of collaborator to invite.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateCollaborationRequestBodyAccessibleByTypeField>))]
        public StringEnum<CreateCollaborationRequestBodyAccessibleByTypeField> Type { get; }

        /// <summary>
        /// The ID of the user or group.
        /// 
        /// Alternatively, use `login` to specify a user by email
        /// address.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The email address of the user to grant access to the item.
        /// 
        /// Alternatively, use `id` to specify a user by user ID.
        /// </summary>
        [JsonPropertyName("login")]
        public string? Login { get; init; }

        public CreateCollaborationRequestBodyAccessibleByField(CreateCollaborationRequestBodyAccessibleByTypeField type) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal CreateCollaborationRequestBodyAccessibleByField(StringEnum<CreateCollaborationRequestBodyAccessibleByTypeField> type) {
            Type = type;
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