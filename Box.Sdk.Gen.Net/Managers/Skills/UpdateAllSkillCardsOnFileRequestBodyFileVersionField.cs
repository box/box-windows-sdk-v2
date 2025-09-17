using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateAllSkillCardsOnFileRequestBodyFileVersionField : ISerializable {
        /// <summary>
        /// The value will always be `file_version`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UpdateAllSkillCardsOnFileRequestBodyFileVersionTypeField>))]
        public StringEnum<UpdateAllSkillCardsOnFileRequestBodyFileVersionTypeField>? Type { get; init; }

        /// <summary>
        /// The ID of the file version.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public UpdateAllSkillCardsOnFileRequestBodyFileVersionField() {
            
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