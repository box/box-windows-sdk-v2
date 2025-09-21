using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateAllSkillCardsOnFileRequestBody : ISerializable {
        /// <summary>
        /// Defines the status of this invocation. Set this to `success` when setting Skill cards.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<UpdateAllSkillCardsOnFileRequestBodyStatusField>))]
        public StringEnum<UpdateAllSkillCardsOnFileRequestBodyStatusField> Status { get; }

        /// <summary>
        /// The metadata to set for this skill. This is a list of
        /// Box Skills cards. These cards will overwrite any existing Box
        /// skill cards on the file.
        /// </summary>
        [JsonPropertyName("metadata")]
        public UpdateAllSkillCardsOnFileRequestBodyMetadataField Metadata { get; }

        /// <summary>
        /// The file to assign the cards to.
        /// </summary>
        [JsonPropertyName("file")]
        public UpdateAllSkillCardsOnFileRequestBodyFileField File { get; }

        /// <summary>
        /// The optional file version to assign the cards to.
        /// </summary>
        [JsonPropertyName("file_version")]
        public UpdateAllSkillCardsOnFileRequestBodyFileVersionField? FileVersion { get; init; }

        /// <summary>
        /// A descriptor that defines what items are affected by this call.
        /// 
        /// Set this to the default values when setting a card to a `success`
        /// state, and leave it out in most other situations.
        /// </summary>
        [JsonPropertyName("usage")]
        public UpdateAllSkillCardsOnFileRequestBodyUsageField? Usage { get; init; }

        public UpdateAllSkillCardsOnFileRequestBody(UpdateAllSkillCardsOnFileRequestBodyStatusField status, UpdateAllSkillCardsOnFileRequestBodyMetadataField metadata, UpdateAllSkillCardsOnFileRequestBodyFileField file) {
            Status = status;
            Metadata = metadata;
            File = file;
        }
        
        [JsonConstructorAttribute]
        internal UpdateAllSkillCardsOnFileRequestBody(StringEnum<UpdateAllSkillCardsOnFileRequestBodyStatusField> status, UpdateAllSkillCardsOnFileRequestBodyMetadataField metadata, UpdateAllSkillCardsOnFileRequestBodyFileField file) {
            Status = status;
            Metadata = metadata;
            File = file;
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