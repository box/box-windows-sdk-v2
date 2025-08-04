using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenTagV2025R0 : ISerializable {
        /// <summary>
        /// The content of the tag.
        /// </summary>
        [JsonPropertyName("tag_content")]
        public string TagContent { get; }

        /// <summary>
        /// Type of the tag.
        /// </summary>
        [JsonPropertyName("tag_type")]
        [JsonConverter(typeof(StringEnumConverter<DocGenTagV2025R0TagTypeField>))]
        public StringEnum<DocGenTagV2025R0TagTypeField> TagType { get; }

        /// <summary>
        /// List of the paths.
        /// </summary>
        [JsonPropertyName("json_paths")]
        public IReadOnlyList<string> JsonPaths { get; }

        public DocGenTagV2025R0(string tagContent, DocGenTagV2025R0TagTypeField tagType, IReadOnlyList<string> jsonPaths) {
            TagContent = tagContent;
            TagType = tagType;
            JsonPaths = jsonPaths;
        }
        
        [JsonConstructorAttribute]
        internal DocGenTagV2025R0(string tagContent, StringEnum<DocGenTagV2025R0TagTypeField> tagType, IReadOnlyList<string> jsonPaths) {
            TagContent = tagContent;
            TagType = tagType;
            JsonPaths = jsonPaths;
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