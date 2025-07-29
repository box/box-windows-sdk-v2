using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class Classification : ISerializable {
        /// <summary>
        /// The name of the classification applied to the item.
        /// </summary>
        [JsonPropertyName("Box__Security__Classification__Key")]
        public string? BoxSecurityClassificationKey { get; init; }

        /// <summary>
        /// The identifier of the item that this metadata instance
        /// has been attached to. This combines the `type` and the `id`
        /// of the parent in the form `{type}_{id}`.
        /// </summary>
        [JsonPropertyName("$parent")]
        public string? Parent { get; init; }

        /// <summary>
        /// The value will always be `securityClassification-6VMVochwUWo`.
        /// </summary>
        [JsonPropertyName("$template")]
        [JsonConverter(typeof(StringEnumConverter<ClassificationTemplateField>))]
        public StringEnum<ClassificationTemplateField>? Template { get; init; }

        /// <summary>
        /// The scope of the enterprise that this classification has been
        /// applied for.
        /// 
        /// This will be in the format `enterprise_{enterprise_id}`.
        /// </summary>
        [JsonPropertyName("$scope")]
        public string? Scope { get; init; }

        /// <summary>
        /// The version of the metadata instance. This version starts at 0 and
        /// increases every time a classification is updated.
        /// </summary>
        [JsonPropertyName("$version")]
        public long? Version { get; init; }

        /// <summary>
        /// The unique ID of this classification instance. This will be include
        /// the name of the classification template and a unique ID.
        /// </summary>
        [JsonPropertyName("$type")]
        public string? Type { get; init; }

        /// <summary>
        /// The version of the metadata template. This version starts at 0 and
        /// increases every time the template is updated. This is mostly for internal
        /// use.
        /// </summary>
        [JsonPropertyName("$typeVersion")]
        public double? TypeVersion { get; init; }

        /// <summary>
        /// Whether an end user can change the classification.
        /// </summary>
        [JsonPropertyName("$canEdit")]
        public bool? CanEdit { get; init; }

        public Classification() {
            
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