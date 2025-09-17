using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateAllSkillCardsOnFileRequestBodyFileField : ISerializable {
        /// <summary>
        /// The value will always be `file`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UpdateAllSkillCardsOnFileRequestBodyFileTypeField>))]
        public StringEnum<UpdateAllSkillCardsOnFileRequestBodyFileTypeField> Type { get; set; }

        /// <summary>
        /// The ID of the file.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public UpdateAllSkillCardsOnFileRequestBodyFileField() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}