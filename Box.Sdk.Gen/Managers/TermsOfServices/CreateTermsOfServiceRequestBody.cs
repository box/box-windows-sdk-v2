using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateTermsOfServiceRequestBody : ISerializable {
        /// <summary>
        /// Whether this terms of service is active.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<CreateTermsOfServiceRequestBodyStatusField>))]
        public StringEnum<CreateTermsOfServiceRequestBodyStatusField> Status { get; }

        /// <summary>
        /// The type of user to set the terms of
        /// service for.
        /// </summary>
        [JsonPropertyName("tos_type")]
        [JsonConverter(typeof(StringEnumConverter<CreateTermsOfServiceRequestBodyTosTypeField>))]
        public StringEnum<CreateTermsOfServiceRequestBodyTosTypeField>? TosType { get; init; }

        /// <summary>
        /// The terms of service text to display to users.
        /// 
        /// The text can be set to empty if the `status` is set to `disabled`.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; }

        public CreateTermsOfServiceRequestBody(CreateTermsOfServiceRequestBodyStatusField status, string text) {
            Status = status;
            Text = text;
        }
        
        [JsonConstructorAttribute]
        internal CreateTermsOfServiceRequestBody(StringEnum<CreateTermsOfServiceRequestBodyStatusField> status, string text) {
            Status = status;
            Text = text;
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