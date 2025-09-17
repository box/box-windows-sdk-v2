using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataBase : ISerializable {
        /// <summary>
        /// The identifier of the item that this metadata instance
        /// has been attached to. This combines the `type` and the `id`
        /// of the parent in the form `{type}_{id}`.
        /// </summary>
        [JsonPropertyName("$parent")]
        public string? Parent { get; init; }

        /// <summary>
        /// The name of the template.
        /// </summary>
        [JsonPropertyName("$template")]
        public string? Template { get; init; }

        /// <summary>
        /// An ID for the scope in which this template
        /// has been applied. This will be `enterprise_{enterprise_id}` for templates
        /// defined for use in this enterprise, and `global` for general templates
        /// that are available to all enterprises using Box.
        /// </summary>
        [JsonPropertyName("$scope")]
        public string? Scope { get; init; }

        /// <summary>
        /// The version of the metadata instance. This version starts at 0 and
        /// increases every time a user-defined property is modified.
        /// </summary>
        [JsonPropertyName("$version")]
        public long? Version { get; init; }

        public MetadataBase() {
            
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