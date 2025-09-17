using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class CollaboratorVariableVariableValueField : ISerializable {
        /// <summary>
        /// The object type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CollaboratorVariableVariableValueTypeField>))]
        public StringEnum<CollaboratorVariableVariableValueTypeField> Type { get; }

        /// <summary>
        /// User's ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public CollaboratorVariableVariableValueField(string id, CollaboratorVariableVariableValueTypeField type = CollaboratorVariableVariableValueTypeField.User) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal CollaboratorVariableVariableValueField(string id, StringEnum<CollaboratorVariableVariableValueTypeField> type) {
            Type = CollaboratorVariableVariableValueTypeField.User;
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