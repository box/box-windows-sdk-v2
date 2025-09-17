using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateTermsOfServiceStatusForUserRequestBodyUserField : ISerializable {
        /// <summary>
        /// The type of object.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateTermsOfServiceStatusForUserRequestBodyUserTypeField>))]
        public StringEnum<CreateTermsOfServiceStatusForUserRequestBodyUserTypeField> Type { get; }

        /// <summary>
        /// The ID of user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public CreateTermsOfServiceStatusForUserRequestBodyUserField(string id, CreateTermsOfServiceStatusForUserRequestBodyUserTypeField type = CreateTermsOfServiceStatusForUserRequestBodyUserTypeField.User) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal CreateTermsOfServiceStatusForUserRequestBodyUserField(string id, StringEnum<CreateTermsOfServiceStatusForUserRequestBodyUserTypeField> type) {
            Type = CreateTermsOfServiceStatusForUserRequestBodyUserTypeField.User;
            Id = id;
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