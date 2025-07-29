using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class StatusSkillCard : ISerializable {
        /// <summary>
        /// The optional date and time this card was created at.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The value will always be `skill_card`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<StatusSkillCardTypeField>))]
        public StringEnum<StatusSkillCardTypeField> Type { get; }

        /// <summary>
        /// The value will always be `status`.
        /// </summary>
        [JsonPropertyName("skill_card_type")]
        [JsonConverter(typeof(StringEnumConverter<StatusSkillCardSkillCardTypeField>))]
        public StringEnum<StatusSkillCardSkillCardTypeField> SkillCardType { get; }

        /// <summary>
        /// The title of the card.
        /// </summary>
        [JsonPropertyName("skill_card_title")]
        public StatusSkillCardSkillCardTitleField? SkillCardTitle { get; init; }

        /// <summary>
        /// Sets the status of the skill. This can be used to show a message to the user while the Skill is processing the data, or if it was not able to process the file.
        /// </summary>
        [JsonPropertyName("status")]
        public StatusSkillCardStatusField Status { get; }

        /// <summary>
        /// The service that applied this metadata.
        /// </summary>
        [JsonPropertyName("skill")]
        public StatusSkillCardSkillField Skill { get; }

        /// <summary>
        /// The invocation of this service, used to track
        /// which instance of a service applied the metadata.
        /// </summary>
        [JsonPropertyName("invocation")]
        public StatusSkillCardInvocationField Invocation { get; }

        public StatusSkillCard(StatusSkillCardStatusField status, StatusSkillCardSkillField skill, StatusSkillCardInvocationField invocation, StatusSkillCardTypeField type = StatusSkillCardTypeField.SkillCard, StatusSkillCardSkillCardTypeField skillCardType = StatusSkillCardSkillCardTypeField.Status) {
            Type = type;
            SkillCardType = skillCardType;
            Status = status;
            Skill = skill;
            Invocation = invocation;
        }
        
        [JsonConstructorAttribute]
        internal StatusSkillCard(StatusSkillCardStatusField status, StatusSkillCardSkillField skill, StatusSkillCardInvocationField invocation, StringEnum<StatusSkillCardTypeField> type, StringEnum<StatusSkillCardSkillCardTypeField> skillCardType) {
            Type = StatusSkillCardTypeField.SkillCard;
            SkillCardType = StatusSkillCardSkillCardTypeField.Status;
            Status = status;
            Skill = skill;
            Invocation = invocation;
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