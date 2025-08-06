using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInput : SignRequestPrefillTag, ISerializable {
        /// <summary>
        /// Type of input.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputTypeField>))]
        public StringEnum<SignRequestSignerInputTypeField>? Type { get; init; }

        /// <summary>
        /// Content type of input.
        /// </summary>
        [JsonPropertyName("content_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputContentTypeField>))]
        public StringEnum<SignRequestSignerInputContentTypeField>? ContentType { get; init; }

        /// <summary>
        /// Index of page that the input is on.
        /// </summary>
        [JsonPropertyName("page_index")]
        public long PageIndex { get; }

        /// <summary>
        /// Whether this input was defined as read-only(immutable by signers) or not.
        /// </summary>
        [JsonPropertyName("read_only")]
        public bool? ReadOnly { get; init; }

        public SignRequestSignerInput(long pageIndex) {
            PageIndex = pageIndex;
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}